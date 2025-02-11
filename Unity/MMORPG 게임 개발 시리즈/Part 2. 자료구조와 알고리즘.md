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