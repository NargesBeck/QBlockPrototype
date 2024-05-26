using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private enum TouchStates { Idle, PlacingPattern }

    private TouchStates TouchState = TouchStates.Idle;

    private void Update()
    {
        if (TouchState == TouchStates.Idle && Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            RaycastHit hit;
            if (Physics.Raycast(mousePos, Vector3.forward, out hit))
            {
                if (hit.collider.gameObject.tag == "Pattern")
                {
                    TouchState = TouchStates.PlacingPattern;
                    GameManager.Instance.CurrentlySelectedPattern = hit.collider.gameObject.GetComponent<Pattern>();
                }
            }
        }

        if (TouchState == TouchStates.PlacingPattern && Input.GetMouseButtonUp(0)) 
        {
            TouchState = TouchStates.Idle;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            RaycastHit hit;
            if (Physics.Raycast(mousePos, Vector3.forward, out hit))
            {
                if (hit.collider.gameObject.tag == "Tile")
                {
                    TouchState = TouchStates.PlacingPattern;

                    GameManager.Instance.BoardManager.PlacePattern(hit.collider.gameObject.name);
                }
            }
        }
    }
}
