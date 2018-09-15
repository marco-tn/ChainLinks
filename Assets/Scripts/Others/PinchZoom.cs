using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float orthoZoomSpeed = 1f;        // 平行投影モードでの平行投影サイズの変化の速さ

    public Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }


    void Update()
    {
        // 端末に 2 つのタッチがあるならば...
        if (Input.touchCount == 2)
        {
            // 両方のタッチを格納します
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 各タッチの前フレームでの位置をもとめます
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 各フレームのタッチ間のベクター (距離) の大きさをもとめます
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 各フレーム間の距離の差をもとめます
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


            // ... タッチ間の距離の変化に基づいて平行投影サイズを変更します
            camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed / 10;

            // 平行投影サイズが決して 0 未満にならないように気を付けてください
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 3f);
            camera.orthographicSize = Mathf.Min(camera.orthographicSize, 15f);

        }
        // 端末に 3 つのタッチがあるならば...
        if (Input.touchCount == 3)
        {
            // 両方のタッチを格納します
            Touch touchZero3 = Input.GetTouch(0);

            Vector3 pos = this.transform.position;

            // ... タッチ間の距離の変化に基づいて平行投影サイズを変更します
            pos.x -= touchZero3.deltaPosition.x / 30;
            pos.y -= touchZero3.deltaPosition.y / 30;

            this.transform.position = pos;

        }

    }
}