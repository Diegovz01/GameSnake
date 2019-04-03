using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Points pointsAdd;
    public GameObject Block;
    public GameObject Item;
    public GameObject Stage;
    public int Width, High;
    public float ForceExplosion = 200f;

    Queue<GameObject> body = new Queue<GameObject>(); // Lista Cola
    GameObject head;
    GameObject item;

    Vector3 direction = Vector3.right;

    enum TypeBox
    {
        Empty, Obstacle, Item
    }
    TypeBox[,] map;

    public void Awake()
    {
        map = new TypeBox[Width, High];
        CreateWalls();
        pointsAdd = GameObject.Find("Canvas_Points").GetComponent<Points>();
    }

    public void StartMoveGame()
    {
        int positionInitialX = Width / 2;
        int positionInitialY = High / 2;

        for (int c = 5; c > 0; c--)
        {
            NewBlock(positionInitialX - c, positionInitialY);
        }
        head = NewBlock(positionInitialX, positionInitialY);
        InstanceItemRandomPosition();
        StartCoroutine(Movement());
    }

    private void InstanceItemRandomPosition()
    {
        Vector2Int position = GetPositionEmpty();
        item = NewItem(position.x, position.y);
    }

    private Vector2Int GetPositionEmpty()
    {
        List<Vector2Int> positionEmpty = new List<Vector2Int>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < High; y++)
            {
                if (map[x, y] == TypeBox.Empty)
                {
                    positionEmpty.Add(new Vector2Int(x, y));
                }
            }
        }
        return positionEmpty[Random.Range(0, positionEmpty.Count)];
    }

    private GameObject NewItem(float x, float y)
    {
        GameObject newItem = Instantiate(Item, new Vector3(x, y), Quaternion.identity, Stage.transform);
        SetMap(newItem.transform.position, TypeBox.Item);
        return newItem;
    }

    TypeBox GetMap(Vector3 position)
    {
        return map[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)];
    }   // RoundToInt() => Convierte a entero

    void SetMap(Vector3 position, TypeBox value)
    {
        map[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = value;
    }

    private IEnumerator Movement()
    {
        WaitForSeconds wait = new WaitForSeconds(0.15f);
        while (true)
        {
            Vector3 newPosition = head.transform.position + direction;
            TypeBox boxBusy = GetMap(newPosition);

            if (boxBusy == TypeBox.Obstacle)
            {
                Dead();
                yield return new WaitForSeconds(3);
                GameManager.sharedInstance.GameOver();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                yield break;
            }
            else
            {
                GameObject bodyPart;
                if (boxBusy == TypeBox.Item)
                {
                    bodyPart = NewBlock(newPosition.x, newPosition.y);
                    MoveItemPositionRandom();
                    pointsAdd.AddPoints();
                }
                else
                {
                    bodyPart = body.Dequeue();
                    SetMap(bodyPart.transform.position, TypeBox.Empty);
                    bodyPart.transform.position = newPosition;
                    SetMap(newPosition, TypeBox.Obstacle);
                    body.Enqueue(bodyPart);
                }

                head = bodyPart;

                yield return wait;
            }
        }
    }

    private void Dead()
    {
        Explosion(this.GetComponentsInChildren<Rigidbody>());
        Explosion(Stage.GetComponentsInChildren<Rigidbody>());

        Camera.main.backgroundColor = Color.red;
    }

    private void Explosion(Rigidbody[] rigidbodys)
    {
        foreach (Rigidbody r in rigidbodys)
        {
            r.useGravity = true;
            r.AddForce(Random.insideUnitCircle.normalized * ForceExplosion);
            r.AddTorque(0, 0, Random.Range(-ForceExplosion, ForceExplosion));
        }
    }

    private void MoveItemPositionRandom()
    {
        Vector2Int position = GetPositionEmpty();
        item.transform.position = new Vector3(position.x, position.y);
        SetMap(item.transform.position, TypeBox.Item);
    }

    private GameObject NewBlock(float x, float y)
    {
        GameObject newBlock = Instantiate(Block, new Vector3(x, y), Quaternion.identity, this.transform);
        body.Enqueue(newBlock); // Enqueue() => añadir bloque
        SetMap(newBlock.transform.position, TypeBox.Obstacle);
        return newBlock;
    }

    private void CreateWalls()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < High; y++)
            {
                // Condicion para poner bloques en bordes
                if (x == 0 || x == Width - 1 || y == 0 || y == High - 1)
                {
                    Vector3 position = new Vector3(x, y);
                    Instantiate(Block, position, Quaternion.identity, Stage.transform);
                    SetMap(position, TypeBox.Obstacle);
                }
            }
        }
    }

    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 directionSelected = new Vector3(horizontal, vertical);

            if (directionSelected != Vector3.zero)
            {
                direction = directionSelected;
            }
        }
    }
}
