## 1. 개론

- 길찾기 알고리즘을 만들며 여러가지 자료구조와 알고리즘을 학습하는 것이 목표
- Big-O 표기법을 통해 시간복잡도나 공간복잡도를 표현할 수 있다.
    - 만약 알고리즘의 효율을 시간적으로 비교하기 위해 사용되는 표현법이다.
    - 한 번의 반복만에 알고리즘이 return된다면 O(1)
    - Input이 N일 때, 반복이 N번 실행되고 return된다면 O(n)
    - Input이 N일 때, 이중 루프를 돌고 return된다면 O(n^2)
    - 조급 더 자세하기 표기한다면 O(3N + 2) 처럼 적힐 수 있지만, 여기서 상수는 그 영향이 그렇게 크지 않으므로 앞 계수와 뒤 상수를 제거하여 O(N)으로 적는다. 물론 상수 계수가 높은 알고리즘과 상수 계수가 낮은 알고리즘이 둘 다 O(N)으로 표기될 수 있다.
- 주로 자주 사용되는 Big-O 표기법 안에 들어가는 내용이 정해져있다.
    - 1 < log n < n < n log n < n^2
    - 여기서 주로 보이는 log 함수는 제곱의 반대의 의미인 수학적인 log와 유사하며, 효율이 좋은 소팅 알고리즘에서 주로 보이는 시간복잡도이다.
- 1에서 100 사이의 숫자 중 하나가 정답이고, 그 정답을 맞추는 알고리즘을 생각해보자.
    - 1부터 100까지 순서대로 전부 말해본다면 O(n)이 소요된다.
    - 50을 먼저 말하고 up / down을 들으며 이분 탐색을 한다면 O(log n)이 소요된다.
- Big-O 표기법은 웬만하면 해당 알고리즘의 최악의 경우를 따진다.
    - 그러나 퀵 정렬처럼 최악의 경우가 이론적으로 거의 등장하지 않는다면 무시하기도 한다.
```csharp
Main()
{
	Console.CursorVisible = false;
	
	const int WAIT_TICK = 1000 / 30; // 1/30초
	const char CIRCLE = '\u25cf';
	
	int lastTick = 0; // 마지막 시간
	while (true)
	{
		#region 프레임 관리
		int currentTick = System.Environment.TickCount;
		if (currentTick - lastTick < WAIT_TICK)
			continue; // 연산 프레임이 되지 않았으므로 다음 프레임으로 스킵
			
		lastTick = currentTick;
		#endregion
		
		// 입력
		
		// 로직
		
		// 렌더링
		Console.SetCursorPosition(0, 0);
		
		for (int i = 0; i < 25; i++)
		{
			for (int j = 0; j < 25; j++)
			{
				Console.ForegroundColor = ConsoleColor.Green; // 색 결정
				Console.Write(CIRCLE); // 쓰기
			}
			Console.WriteLine();
		}
	}
}
```

- 위와 같은 기본적인 코드를 두고 시작한다.

## 2. 선형 자료 기초

### 배열, 동적 배열, 연결 리스트 비교

- 배열, 연결 리스트, 스택과 큐는 일렬로 데이터가 쭉 나열되어있다.
- 반대로 비선형 구조는 트리와 그래프가 존재한다. 비선형 자료구조는 일대다로 연결되어있다.
- 배열
    - 사용할 용량을 고정된 크기만큼 계약하여 사용하고, 절대 그 크기를 변경할 수 없다.
    - 연속된 공간을 배정받아 사용하기 때문에 메모리상에서 연속적으로 존재한다.
- 동적 배열
    - 사용할 용량을 유동적으로 계약받을 수 있고, 연속된 방으로 받을 수 있다.
    - 확장이 필요하다면 리사이징 과정을 통해 크기를 늘릴 수 있다.
    - 단, 리사이징 과정에서 꽤 많은 연산 비용이 들 수 있다.
    - 그래서 보통 리사이징을 할 때는 1.5배에서 2배까지 크기를 키워 리사이징 횟수를 최소화한다.
    - 그러나 중간에 데이터를 삽입하거나, 중간에 있는 데이터를 삭제하기가 어렵다.
- 연결 리스트 (ADT)
    - 연속되지 않은 방을 사용하고, 한 방이 다음 방의 주소를 알고 있다.
    - 중간에 데이터를 추가하고 삭제하는것이 매우 쉬우며, 크기를 늘리기도 쉽다.
    - 그러나 N번째 공간을 O(1)의 속도로 참조할 수 없으며, 처음부터 순차적으로 접근해야 한다.

### 동적 배열 구현 연습

```csharp
public int[] _data = new int[25]; // 배열
public List<int> _data2 = new List<int>(); // 동적 배열
public LinkedList<int> _data3 = new LinkedList<int>(); // 연결 리스트 
```

- 맵이 실시간으로 바뀌는 것이 아닌 이상, 보통 고정적이기 때문에 삽입 / 삭제가 효율이 좋은 연결 리스트는 맵을 만들 때 적합하지 않다.
- 동적 배열의 리사이징은 단점이지만, 맵의 크기가 게임 도중 늘어났다 줄어드는 일이 크게 없기 때문에 일반 정적 배열이 가장 효율이 좋다.
```csharp
_data2.Add(101);
_data2.Add(102);
_data2.Add(103);
_data2.Add(104);
_data2.Add(105);

int temp = _data2[2]; // 탐색
_data2.RemoveAt(2); // 삭제
```

- 동적 배열은 위와 같이 삽입, 탐색, 삭제를 할 수 있다.
- 동적 배열을 직접 클래스와 제네릭을 활용해 만들 수 있다.

```csharp
class MyList<T>
{
    const int DEFAULT_SIZE = 1;
    T[] _data = new T[DEFAULT_SIZE]; // 기본 배열 초기화

    public int Count = 0; // 실제로 사용중인 데이터 개수
    public int Capacity { get { return _data.Length; } } // 예약된 데이터 개수
}
```

- Count와 Capacity는 실제로 동적 배열을 만들 때 오피셜로 사용하는 변수명이다.
- 동적 배열에 데이터를 넣는 과정과, 리사이징을 직접 구현할 수 있다.
```csharp
public void Add(T item)
{
    // 1. 공간이 충분히 남아있는지 확인
    if (Count >= Capacity)
    {
        // 리사이징을 통해 공간 확보
        T[] newArray = new T[Count * 2];
        for (int i = 0; i < Count; i++)
            newArray[i] = _data[i];

        _data = newArray; // 원본에 새로운 배열을 삽입
    }

    // 2. 공간에 데이터를 삽입
    _data[Count] = item;
    Count++; // 데이터 개수 증가
}
```

- 동적 배열에서 데이터를 가져오는 것을 Index와 프로시저를 활용해 구현할 수 있다.
```csharp
public T this[int index] // 인덱스
{
    get { return _data[index]; }
    set { _data[index] = value; }
}
```

- 동적 배열에서 데이터를 삭제하는 것을 구현할 수 있다.
```csharp
public void RemoveAt(int index) // 주소를 활용해 삭제
{
    for (int i = index; i < Count - 1; i++)
    {
        _data[i] = _data[i + 1]; // 뒤에있는 값들을 하나씩 앞으로 끌고오기
    }

    _data[Count - 1] = default(T); // 가장 마지막 원소를 기본값으로 초기화
    Count--;
}
```

- 이렇게 만들어진 동적 배열은 실제로 사용할 수 있다.

```csharp
public MyList<int> _data = new MyList<int>();
```
- 이렇게 내가 직접 만든 동적 배열을 시간 복잡도를 체크할 수 있다.
- 많은 사람들이 오해할 수 있는 부분이, 리사이징을 할 때 for문이 N만큼 회전하기 때문에 동적 배열의 삽입 시간 복잡도가 O(n)이라고 생각하기 쉽다.
- 그러나 이는 Count가 Capacity보다 클 때 일어나고, 이런 경우는 거시적으로 봤을 때 정말 없기 때문에 우리는 O(1)이라고 동적 배열의 삽입 시간 복잡도를 약속했다.
- 오히려 배열에서 데이터를 삭제하는 과정이 모든 데이터를 한 칸씩 앞으로 이동시켜야 하기 때문에 O(n)이 소요된다.

### 연결 리스트 구현 연습

```csharp
public int[] _data = new int[25]; // 배열
public LinkedList<int> _data3 = new LinkedList<int>(); // 연결 리스트 

public void Initialize()
{
    LinkedListNode<int> node = _data3.AddLast(10);

    _data3.Remove(node);
}
```

- 링크드 리스트는 배열과 다르게 도중 삽입과 삭제가 쉽다.
- 원하는 노드 부분을 LinkedListNode라는 자료형의 데이터에 삽입하고, 삭제하면 빠르게 해당 연결 리스트에서 사라진다.
- 연결 리스트는 모든 노드들의 위치가 전혀 연결되어있지 않으니 가능하다.

```csharp
class Room<T>
{
    public T Data;
    Room<T> Next;
    Room<T> Prev;
}
```

- 모든 노드가 이어져있을 수 있기 때문에 위와 같이 설계한다.

```csharp
class RoomList<T>
{
    public Room<T> Head = null;
    public Room<T> Tail = null;
    public int Count = 0;

    public Room<T> AddLast(T data)
    {
        Room<T> newRoom = new Room<T>();
        newRoom.Data = data;

        // 방이 하나도 없다면 헤드로 설정
        if (Head != null)
        {
            Head = newRoom;
        }

        // 기존의 방과 새로운 방 연결
        if (Tail != null)
        {
            Tail.Next = newRoom;
            newRoom.Prev = Tail;
        }

        Tail = newRoom; // 꼬리 방 설정
        Count++;

        return newRoom;
    }
}
```

- 연결 리스트 클래스를 만들어 다음과 같이 다음 방 추가를 구현할 수 있다.
- Head와 Tail을 항상 관리하며, 방이 2개 이상 있다면 이전 방과 다음 방의 주소를 연결해야 한다.

```csharp
public void Remove(Room<T> room)
{
    if (Head == room)
    {
        // 다음 방이 첫번째 방이 됨
        Head = Head.Next;
    }

    if (Tail == room)
    {
        // 이제부터 마지막 방이 마지막 방 이전 방이 됨
        Tail = Tail.Prev;
    }

    // 앞뒤의 두 방을 서로가 연결되게 수정
    if (room.Prev != null)
    {
        room.Prev.Next = room.Next;
    }

    if (room.Next != null)
    {
        room.Next.Prev = room.Prev;
    }
}
```

- 노드 제거의 경우엔 실제로 데이터에서 할당을 해제하는 복잡한 것 보다는, 연결리스트의 핵심적인 주소 변경을 확인해야 한다.
- 헤드와 테일이 삭제되는지 우선 보고, 추후에 해당 노드의 앞뒤에 있는 노드의 주소를 연결한다.
- 연결 리스트는 동적 배열과 다르게 삽입과 삭제에 for문이 하나도 없다. 데이터의 주소들을 앞이나 뒤로 당길 필요가 전혀 없기 때문이다.
    - 즉, 삽입과 삭제의 소요 시간이 O(1)이다.
- 단, 중간 접근은 지원하지 않기 때문에, 최악의 경우 O(n)이 소요될 수 있다. 배열은 빠른 접근이 가능한 것과 비교된다.

## 3. 미로 준비

### 맵 만들기

```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Text;

namespace Algorithm
{
    class Board
    {
        public enum TileType
        {
            Empty,
            Wall
        }

        const char CIRCLE = '\u25cf';
        public TileType[,] _tile;
        public int _size;

        public void Initialize(int size)
        {
            _tile = new TileType[size, size];
            _size = size;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x == 0 || x == _size - 1 || y == 0 || y == _size - 1)
                    {
                        _tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        _tile[y, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    Console.Write(CIRCLE); // 쓰기
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}

```
```csharp
using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize(25);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30; // 1/30초
            

            int lastTick = 0; // 마지막 시간
            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)
                    continue; // 연산 프레임이 되지 않았으므로 다음 프레임으로 스킵

                lastTick = currentTick;
                #endregion

                // 입력

                // 로직

                // 렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}

```

### Binary Tree 미로 생성 알고리즘

- Mazes for Programmers 저서에 나오는 가장 대표적인 2가지 알고리즘 구현 목표
- x나 y 좌표가 짝수인 경우에 모두 벽으로 밀어버리는 것으로 시작

```csharp
private void GenerateByBinaryTree()
{
    for (int y = 0; y < _size; y++)
    {
        for (int x = 0; x < _size; x++)
        {
            if (x % 2 == 0 || y % 2 == 0)
            {
                _tile[y, x] = TileType.Wall;
            }
            else
            {
                _tile[y, x] = TileType.Empty;
            }
        }
    }

    // 랜덤으로 우측 혹은 아래로 길을 뚫는다
    Random rand = new Random();
    for (int y = 0; y < _size; y++)
    {
        for (int x = 0; x < _size; x++)
        {
            if (x % 2 == 0 || y % 2 == 0)
            {
                continue;
            }

            if (x == _size - 2 && y == _size - 2)
                continue;

            if (y == _size - 2)
            {
                _tile[y, x + 1] = TileType.Empty;
                continue;
            }

            if (x == _size - 2)
            {
                _tile[y + 1, x] = TileType.Empty;
                continue;
            }

            if (rand.Next(0, 2) == 0)
            {
                _tile[y, x + 1] = TileType.Empty;
            }
            else
            {
                _tile[y + 1, x] = TileType.Empty;
            }
        }
    }
}
```

- 랜덤으로 우측 혹은 아래의 길을 뚫는 작업으로 미로를 구성한다.
- 단 이럴 경우 가장 아래와 가장 우측이 막히려면 반드시 한 쪽으로 뚫리도록 조건문을 추가해야하고, 그러다보니 맵의 가장 오른쪽 줄과 아랫줄은 쭉 뚫려있는 단점이 존재한다.

### SideWinder 미로 생성 알고리즘

- 바이너리 트리와 다르게, 일단 1/2 확률로 연산을 하는 것은 마찬가지이나, 아래로 길을 뚫어야 하는 일이 생긴다면, 이전까지 모든 노드들 중에서 랜덤한 하나의 노드를 선택해 아래로 뚫는 것이다.

```csharp
private void GenerateBySideWinder()
{
    for (int y = 0; y < _size; y++)
    {
        for (int x = 0; x < _size; x++)
        {
            if (x % 2 == 0 || y % 2 == 0)
            {
                _tile[y, x] = TileType.Wall;
            }
            else
            {
                _tile[y, x] = TileType.Empty;
            }
        }
    }

    // 랜덤으로 우측 혹은 아래로 길을 뚫는다
    Random rand = new Random();
    for (int y = 0; y < _size; y++)
    {
        int count = 1;
        for (int x = 0; x < _size; x++)
        {
            if (x % 2 == 0 || y % 2 == 0)
            {
                continue;
            }

            if (x == _size - 2 && y == _size - 2)
                continue;

            if (y == _size - 2)
            {
                _tile[y, x + 1] = TileType.Empty;
                continue;
            }

            if (x == _size - 2)
            {
                _tile[y + 1, x] = TileType.Empty;
                continue;
            }

            if (rand.Next(0, 2) == 0)
            {
                _tile[y, x + 1] = TileType.Empty;
                count++; // 우측 길을 몇 번 뚫었는지 기록
            }
            else
            {
                int randomIndex = rand.Next(0, count);
                _tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                count = 1;
            }
        }
    }
}
```

- 이전까지 길 중 하나를 랜덤해서 아래로 뚫기 때문에 모양이 더욱 불규칙적이고 이쁘다.
- 단, 최종 벽을 뚫어버리는 문제를 임의로 막았기 때문에 여전히 가장 오른쪽 줄과 아래 줄이 막혀있는 문제가 존재한다.

### 플레이어 이동

- 모든 객체지향 프로그래밍이 으레 그렇듯, 플레이어라는 객체가 수행해야 하는 기능이라면 외부가 아닌 플레이어 클래스 내에 넣는것이 좋다.
- 프로퍼티를 활용해 변수를 자신만 고치고, 남들은 get만 하게 설정한다.
- Update 로직을 만들 때, 그냥 단순히 1틱마다 행동하면 너무 빠르기 때문에 꼭 deltaTick 등의 개념을 넣어 절대 시간 기준을 만드는 것이 좋다.

```csharp
class Player
{
    public int PosY { get; private set; }
    public int PosX { get; private set; }
    Random _random = new Random();

    Board _board;

    public void Initialize(int posY, int posX, int destY, int destX, Board board)
    {
        PosY = posY;
        PosX = posX;

        _board = board;
    }

    const int MOVE_TICK = 100;
    int _sumTick = 0;

    public void Update(int deltaTick)
    {
        _sumTick += deltaTick;
        if (_sumTick >= MOVE_TICK)
        {
            _sumTick = 0;

            // 0.1초마다 실행될 로직
            int randValue = _random.Next(0, 5);
            switch (randValue)
            {
                case 0: // 상
                    if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                        PosY = PosY - 1;
                    break;
                case 1: // 하
                    if (PosY + 1 < _board.Size && _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                        PosY = PosY + 1;
                    break;
                case 2: // 좌
                    if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                        PosX = PosX - 1;
                    break;
                case 3: // 우
                    if (PosX + 1 < _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                        PosX = PosX + 1;
                    break;
            }
        }
    }
}
```

- 배열 내에서 움직일 땐 항상 range 체크를 해서, 이동하고자 하는 좌표가 유효한지 체크해주어야 안전하다.

### 오른손 법칙

- 방향을 Enum 타입으로 정하고, 우수법을 통해 미로를 찾는다.

```csharp
enum Dir
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3,
}

int _dir = (int)Dir.Up;
```

- 방향 전환 코드를 switch case문으로 할 수도 있지만, 아래와 같은 나눗셈 패턴을 쓴다.

```csharp
_dir = (_dir - 1 + 4) % 4;
```

- 우선 %4를 했다면 반드시 0~3이 나오도록 보장할 수 있고, 우리의 목표는 1칸 아래로 까이는 것이다. 여기서 +4를 한 이유는, 만약 0에서 -1이 되어서 -1이 되었을때 이를 3으로 두고 싶으니, 양수의 영역으로 끌어올리기 위해 크기만큼 수를 더하는 것이다.
- 앞으로 한 칸 전진은 자신의 현재 방향을 알아야 가능한데, 이를 쉽게 만들기 위해선 아래와 같은 방향에 따른 좌표 배열을 만들어야 한다.

```csharp
int[] frontY = new int[] { -1, 0, 1, 0 };
int[] frontX = new int[] { 0, -1, 0, 1 };
```

- 이렇게 되면 두 배열의 각 인덱스에 현재 방향을 넣었을 때, 내가 얼마나 이동해야하는지를 바로 알 수 있게 된다.

- 이 길찾기 알고리즘을 초기화 과정 안에 모두 해버리고, 이동한 좌표 데이터를 모두 한 리스트에 넣은 후 렌더링에서 이를 하나씩 그리는 방식으로 구현한다.

```csharp
_points.Add(new Pos(PosY, PosX));

// 목적지 도착 전에는 계속 실행
while (PosY != board.DestY || PosX != board.DestX)
{
    // 1. 현재 바라보는 방향 기준 오른쪽이 갈 수 있는가
    if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
    {
        // 오른쪽 방향으로 90도 회전
        _dir = (_dir - 1 + 4) % 4;

        // 앞으로 전진
        PosY = PosY + frontY[_dir];
        PosX = PosX + frontX[_dir];
        _points.Add(new Pos(PosY, PosX));
    }
    // 2. 현재 바라보는 방향 기준 전진할 수 있는가
    else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
    {
        // 앞으로 전진
        PosY = PosY + frontY[_dir];
        PosX = PosX + frontX[_dir];
        _points.Add(new Pos(PosY, PosX));
    }
    else
    {
        // 왼쪽 방향으로 90도 회전
        _dir = (_dir + 1 + 4) % 4;
    }
}
```

```csharp
public void Update(int deltaTick)
{
    if (_lastIndex >= _points.Count)
        return;

    _sumTick += deltaTick;
    if (_sumTick >= MOVE_TICK)
    {
        _sumTick = 0;

        PosY = _points[_lastIndex].Y;
        PosX = _points[_lastIndex].X;
        _lastIndex++;
    }
}
```

## 4. 그래프

### 스택과 큐

- 위에서 만든 길찾기 알고리즘을 실제로 게임에서 사용하기는 굉장히 무리가 있다.
- 따라서 대부분의 게임 엔진, 및 게임 내 코드에서 길찾기 알고리즘을 사용할 땐 A* 알고리즘을 주로 사용한다.
    - A* 알고리즘을 알기 위해선 그래프 선수지식이 필요하고, 그를 위해선 스택 및 큐 선수지식이 필요하다.
- 둘 다 선형 자료구조이다.
- 스택은 LIFO 자료구조이며, 큐는 FIFO 자료구조이다.
- 두 자료구조 모두 중간에 있는 데이터를 활용하긴 어려우며, 가장 처음이나 마지막 데이터를 활용해야 한다.

```csharp
static void Main(string[] args)
{
    Stack<int> stack = new Stack<int>();
    Queue<int> queue = new Queue<int>();
}
```

- C#은 기본적으로 Stack과 Queue 자료구조를 고맙게도 지원해준다.

```csharp
stack.Push(1);
stack.Push(2);

int data_1 = stack.Peek();
int data_2 = stack.Pop();
```

- 스택의 연산에는 Push와 Pop이 있으며, 가장 위 요소를 삭제는 안하되 보고싶다면 Peek를 사용한다.
    - 스택이 비어있는 경우에 Pop을 하면 크래쉬가 난다.
    - 스택 내 요소들의 개수를 알고싶다면 stack.Count를 사용할 수 있다.

```csharp
queue.Enqueue(1);
queue.Enqueue(2);
queue.Enqueue(3);

int data_3 = queue.Peek();
int data_4 = queue.Dequeue();
```

- 큐는 Enqueue와 Dequeue로 요소를 삽입하고 삭제하며, Peek도 가능하다.
- 링크드 리스트로도 큐나 스택과 같은 연산의 자료구조를 만들 수 있지만, 추상적으로 스택 ADT나 큐 ADT를 사용하는 편이 의사소통이나 표현에 훨씬 이점이 있기 때문에 이를 사용한다.
    - 하지만 단순 List라면 얘기가 조금 다른데, 스택은 문제가 없지만 List로 큐를 만들어버릴 경우 계속해서 빈 공간이 생겨버리기 때문에 이로 큐를 구현하는건 무리가 있다.
    - 실제로 큐를 구현할 땐 순환 버퍼를 활용해 만든다. 원형 큐의 형태를 생각하면 된다.

### 그래프 이론

- 정점(Vertex)과 간선(Edge)으로 이루어진 비선형 자료구조
- 현실 세계의 사물이나 추상적인 개념 간의 연결 관계를 표현
- 단순 그래프(무방향 그래프) 뿐만 아니라, 방향이 있는 그래프와 가중치가 있는 그래프가 존재한다.
    - 가중치 그래프는 확률이나 거리 등이 될 수 있다. 특히 지도나 노선도 등에서 많이 쓰인다.
    - 방향 그래프는 간선에 방향이 있어서 일방통행이 포함된 도로망 등을 표현할 수 있다.
- 그래프를 프로그래밍 언어 상에서 어떻게 표현할까?
    - 가장 직관적인 것은 정점마다 인스턴스를 생성해서 각 인스턴스들끼리 관계를 지정하는 것이다.
        - 그러나 이 방범은 정점을 만들 때마다 인스턴스를 만들어야 하고, 낭비가 심한 구조이다.
    - 리스트를 이용해서 정점을 만드는 방법이 있다.
        - 쉽게 말해 인접 리스트를 만드는 것이다. 각 정점의 리스트 안에 자신과 연결된 정점들의 번호를 적어서 넣어두는 것이다.
    - 방향성 뿐만이 아니라 가중치까지 있다면, 리스트를 활용할 때 조금 더 복잡해진다.
        - 기존의 인접 리스트는 연결된 상대만 정보로 가지고 있었지만,
        - 이제는 연결된 상대와 가중치를 함께 적어야 해서 튜플 등의 자료구조로 함께 목록에 넣어둔다.
        - 그러나 리스트는 메모리를 아끼는 것은 좋으나, 접근 속도에서 비교적 손해를 본다는 단점이 있다.
    - 행렬 (2차원 배열) 을 이용해 표현할 수 있다.
        - 정점 6개가 있다면, 6*6의 행렬을 만들어 서로 연결된 부분에 숫자를 적어 간선을 표현한다.
        - 정점은 적고 간선이 많은 경우 이점이 있고, 빠른 접근이 가능하나 메모리 소모가 심하다는 단점이 있다.
    - 가중치 그래프를 행렬로도 표현할 수 있다.
        - 이 경우 관계가 없을 땐 -1로 표현하고, 그 외엔 가중치를 숫자로 적는다.
        - 즉 연결 유무는 양수/음수로 구분하며, 양수일 땐 가중치로서 알게 된다.