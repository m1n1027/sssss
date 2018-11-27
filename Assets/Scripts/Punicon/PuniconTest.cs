using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    ***参考***
    https://setchi.hatenablog.com/entry/2017/04/08/191455
*/
public class PuniconTest : MonoBehaviour
{
    [Range(1, 30)]
    public float radiusSize = 30;
    public Material material;

    [SerializeField]
    private Camera cam = null;
    private PunipuniMesh mesh;
    private float radius;
    private MeshRenderer meshRenderer = null;
    private MeshFilter meshFilter = null;

    private Vector3 startPosition;
    public punikon Punicon;
    Bezier BezierC = new Bezier(), BezierL = new Bezier(), BezierR = new Bezier();
    public Text debug;
    private LineRenderer line;


    // Use this for initialization
    void Start () {
        SetAspectRait(750, 1334);
        Punicon = FindObjectOfType<punikon>();
        //メッシュの生成
        var p1 = cam.ScreenToWorldPoint(new Vector3(radiusSize, radiusSize, transform.position.z));
        var p2 = cam.ScreenToWorldPoint(new Vector3(-radiusSize, -radiusSize, transform.position.z));
        radius = Mathf.Abs(p1.x - p2.x);

        //mesh = new PunipuniMesh(32, radius);
        mesh = gameObject.AddComponent<PunipuniMesh>();
        var isCreate = mesh.CreateMesh(64, radius);
        //フィルターとマテリアルの設定
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh.Mesh;

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;
        meshRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
        meshRenderer.material = material;
        meshRenderer.enabled = false;
        End();
	}

	void SetAspectRait(float Width, float Height)
    {
        float baseAspectRait = Width / Height;

        //実機のアスペクト比
        float nowAspectRait = (float)Screen.width / (float)Screen.height;

        float aspectRait = baseAspectRait / nowAspectRait;

        //現在のカメラのサイズ
        float size = cam.orthographicSize;

        float deviceAspectRait = 1.0f / aspectRait;
        cam.orthographicSize = size * deviceAspectRait;
    }


    void Begin()
    {

        //startPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var t = Input.GetTouch(0);


        startPosition = cam.ScreenToWorldPoint(t.position);
        startPosition.z = transform.position.z;
        transform.position = startPosition;
        meshRenderer.enabled = true;


    }

    void End()
    {
        meshRenderer.enabled = false;
    }

	// Update is called once per frame
	void Update () {
        if (Punicon.GetPhase() == TouchPhase.Began) Begin();
        if (Punicon.GetPhase() == TouchPhase.Moved) TrackingPunipuni();
        if (Punicon.GetPhase() == TouchPhase.Ended) End();

        if (Input.GetMouseButtonDown(0)) Begin();
        if (Input.GetMouseButtonUp(0)) End();
        if (Input.GetMouseButton(0)) TrackingPunipuni();
    }


    //以下コピペ
    void TrackingPunipuni()
    {
        // ベジェ曲線パラメータの更新
        //var pos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var pos = cam.ScreenToWorldPoint((Vector2)Punicon.GetTouch().position);
        //debug.text = "pos " + transform.position + "\ntouchpos" + cam.ScreenToWorldPoint( (Vector3)Punicon.GetTouch().position );
        var x = this.startPosition.x - pos.x;
        var y = this.startPosition.y - pos.y;

        UpdateBezierParameter(-x, -y);
        // Debug.LogFormat("Mouse X = {0}, y = {1}", Input.mousePosition.x, Input.mousePosition.y);

        // メッシュの更新
        mesh.Vertexes = TransformFromBezier(new Vector3(0, 0, 0));
        var centerPos = BezierC.GetPosition(0.9f);
        mesh.CenterPoint = centerPos;
    }

    private void UpdateBezierParameter(float x, float y)
    {
        AnimateBezierParameter(this.BezierC, this.BezierC, x, y);

        { // 他の2個のベジェの開始位置を更新
            var dir = this.BezierC.P2 - this.BezierC.P1;
            var dirL = new Vector2(dir.y, -dir.x);
            var dirR = new Vector2(-dir.y, dir.x);
            dirL = dirL.normalized;
            dirR = dirR.normalized;
            dirL.x = dirL.x * this.radius + this.BezierC.P1.x;
            dirL.y = dirL.y * this.radius + this.BezierC.P1.y;
            dirR.x = dirR.x * this.radius + this.BezierC.P1.x;
            dirR.y = dirR.y * this.radius + this.BezierC.P1.y;
            this.BezierL.P1 = dirL;
            this.BezierR.P1 = dirR;
        }

        AnimateBezierParameter(this.BezierL, this.BezierC, x, y);
        AnimateBezierParameter(this.BezierR, this.BezierC, x, y);
    }

    float ArrivalTime = 5; // frame count(本来はtimeがいい)

    /// <summary>
    /// ベジェ曲線パラメータを更新します。
    /// </summary>
    /// <param name="bez"></param>
    /// <param name="baseBez"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void AnimateBezierParameter(Bezier bez, Bezier baseBez, float x, float y)
    {
        // 先端点
        bez.P4 = new Vector2(x, y);

        { // 先端制御点
            if (bez.P3 != null)
            {
                var vec = baseBez.P1 - bez.P1;
                vec = vec.normalized * (this.radius / 4);

                var pos = bez.P3 - bez.P4;
                pos += vec;
                pos /= this.ArrivalTime;
                bez.P3 -= pos;
            }
            else
            {
                bez.P3 = new Vector2(x, y);
            }
        }

        { // 中心制御点
          // 最終的な位置
            var ev = baseBez.P4 - baseBez.P1;
            var len = ev.magnitude;
            ev = ev.normalized;
            ev *= (len / 4);
            ev += bez.P1;

            if (bez.P2 != null)
            {
                var v = ev - bez.P2;
                v /= 3; // this.ArrivalTime;
                bez.P2 += v;
                //bez.P2 = ev;
            }
            else
            {
                bez.P2 = new Vector2(bez.P1.x, bez.P1.y);
            }
        }
    }

    /// <summary>
    /// 操作対象の頂点インデックスを取得します。
    /// </summary>
    /// <param name="center"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    void GetMoveFixedVertexIndex(Vector3 center, out int startIndex, out int endIndex)
    {
        Vector3[] points = mesh.OriginalVertexes;

        int sidx = -1;
        int eidx = -1;
        int idx = 0;
        bool recheckStart = true;
        bool recheckEnd = true;
        for (int n = 0; n < points.Length; n++)
        {
            var point = points[n];

            if (BezierL.IsValid)
            {
                var PT = point - center;
                var AB = BezierC.P1 - BezierL.P1;
                var c1 = AB.x * PT.y - AB.y * PT.x;
                if (c1 < 0)
                {
                    // move
                    if (recheckStart)
                    {
                        sidx = idx;
                        recheckStart = false;
                        recheckEnd = true;
                    }
                }
                else
                {
                    // fixed
                    if (recheckEnd)
                    {
                        eidx = idx - 1;
                        recheckStart = true;
                        recheckEnd = false;
                    }
                }
            }
            ++idx;
        }
        startIndex = sidx;
        endIndex = eidx;
    }

    /// <summary>
    /// ベジェ曲線パラメータに応じてメッシュを変形します。
    /// </summary>
    /// <param name="center"></param>
    Vector3[] TransformFromBezier(Vector3 center)
    {
        Vector3[] points = mesh.Vertexes;
        Vector3[] org_points = mesh.OriginalVertexes;

        // 操作対象の頂点インデックスを取得
        int si;
        int ei;
        GetMoveFixedVertexIndex(center, out si, out ei);
        if (ei == -1) ei = points.Length - 1;
        if (si == -1 || ei == -1)
        {
            return org_points;
        }

        if (si > ei) ei += points.Length;
        var useCount = ei - si;
        if (useCount <= 0)
        {
            return org_points;
        }

        int centerIdx = (int)(useCount / 2) + si;
        int count1 = centerIdx - si;
        int count2 = ei - centerIdx;
        // Debug.LogFormat("{0}Num, {1}, {2}Num", count1, centerIdx, count2);

        for (int n = 0; n < points.Length; n++)
        {
            points[n] = org_points[n];
        }

        for (int n = 0; n < count1; n++)
        {
            float t = (float)(n + 1) / (float)(count1 + 1);
            var point = BezierL.GetPosition(t);

            // 半径内にある場合はオリジナルを使用する
            var dist = new Vector3(point.x, point.y, center.z) - center;
            var idx = (n + si) % points.Length;
            if (dist.magnitude > this.radius)
            {
                points[idx].x = point.x;
                points[idx].y = point.y;
            }
        }
        for (int n = 0; n < count2; n++)
        {
            float t = (float)(n + 1) / (float)(count2 + 1);
            var point = BezierR.GetPosition(t);

            // 半径内にある場合はオリジナルを使用する
            var dist = new Vector3(point.x, point.y, center.z) - center;
            var idx = (ei - n) % points.Length;
            if (dist.magnitude > this.radius)
            {
                points[idx].x = point.x;
                points[idx].y = point.y;
            }
        }
        {
            // 半径内にある場合はオリジナルを使用する
            var dist = new Vector3(BezierC.P4.x, BezierC.P4.y, center.z) - center;
            var idx = (centerIdx) % points.Length;
            if (dist.magnitude > this.radius)
            {
                // 中心
                points[idx].x = BezierC.P4.x;
                points[idx].y = BezierC.P4.y;
            }
        }
        return points;
    }

}
