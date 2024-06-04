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

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Pattern")
                {
                    TouchState = TouchStates.PlacingPattern;
                    GameManager.Instance.CurrentlySelectedPattern = hit.collider.gameObject.GetComponent<Pattern>();
                }
            }
        }

        if (TouchState == TouchStates.PlacingPattern)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Tile")
                {
                    GameManager.Instance.BoardManager.PreviewPattern(hit.collider.gameObject.name);
                }
            }
        }

        if (TouchState == TouchStates.PlacingPattern && Input.GetMouseButtonUp(0)) 
        {
            TouchState = TouchStates.Idle;
            GameManager.Instance.BoardManager.PlacePattern(); 
        }
    }
}
