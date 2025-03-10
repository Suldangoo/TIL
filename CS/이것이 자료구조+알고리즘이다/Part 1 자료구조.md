# Part 01 자료구조

- 자료구조란 컴퓨터가 데이터를 효율적으로 다룰 수 있게 도와주는 데이터 보관 방법과 데이터에 관한 연산의 총체를 뜻한다.
    - int와 float같은 단순 자료구조와, 배열, 스택, 그래프같은 복합 자료구조로 구분된다.
    - 복합 자료구조 또한 선형 자료구조와 비선형 자료구조로 구분된다.
- 추상 데이터 형식 (Abstract Data Types) 은 자료구조가 갖춰야 할 일련의 연산들이며, 어떠한 자료구조의 정의와 연산만을 제공하며 그 디테일한 구현 방식은 알려줄 필요가 없는 다양한 자료구조의 혼합체이다.
    - 예를 들어 ADT는 특정 위치에 접근하거나, 뒤에 추가, 중간에 삽입 / 삭제같은 연산을 제공한다.
- 알고리즘이란 어떤 문제를 풀기 위한 단계적 절차이다.
- 알고리즘에 입력을 넣으면, 그 입력값에 따라 다양한 형태의 논리가 돌아가고, 무언가를 출력하게 된다.
- C언어에서 코드를 컴파일하면 실행파일이 만들어지고, 그 파일을 실행하면 메모리 레이아웃이 형성된다. 가장 밑에 텍스트와 데이터의 정적 메모리가 할당되고, 그 위의 모든 공간은 위에서부터 밑으로 스택, 밑에서부터 위로 힙이 차지한다. 힙을 자유 저장소, 스택을 자동 메모리라고도 부른다.
    - 스택은 알아서 데이터를 추가하고 삭제하며 스스로 관리가 된다.
    - 힙은 마음대로 데이터를 할당할 수 있지만, 그만큼 데이터를 다 썼다면 다시 할당을 해제해주어야 한다. 해제를 해주지 않는다면 영구적으로 메모리를 차지하게 된다.
        - 이 명령어는 각각 malloc(), free()이다.

# Chapter 01 : 리스트

## 리스트의 개념

- 리스트 ADT는 목록 형태로 이루어진 데이터며, 개별 요소를 노드라고 부른다.
- 첫 번째 노드를 헤드, 마지막 노드를 테일이라고 한다.
- 리스트에 노드를 추가하거나, 삽입하거나, 제거하거나, 반환하는 연산이 있다.
- 배열은 지정한 시점에서 크기를 지정해주고 더 이상 크기를 바꿀 수 없지만, 리스트는 유연하게 크기를 바꿀 수 있다.

## 연결 리스트

- 노드와 노드들을 연결해 만든 연결 리스트는 한 노드가 다음 노드에 대한 포인터를 가지고 있다.
- 노드 중간에 데이터를 추가하고 삭제하는 것이 매우 쉽다.
- 연결 리스트의 노드를 생성하는 공간에 자동 메모리는 적합하지 않는다. return할 때 자동 메모리의 특성상 이미 제거된 메모리를 참조하기 때문에 어떤 에러를 야기할지 모른다.
- 즉 자유 메모리에 새 노드를 만들 땐 malloc(), 노드를 반환할 땐 free()를 사용하여 SLL을 구현한다.
- 그러나 연결 리스트는 탐색에서 큰 단점을 가진다. 배열은 특정 주소에 O(1)의 시간 복잡도로 접근이 가능하지만, 연결리스트는 모든 노드를 전부 순회하며 노드를 탐색해야 한다.

### 연결 리스트의 장단점

- 단점
    - 다음 노드를 가리키려는 포인터 때문에 각 노드마다 추가적인 메모리가 필요하다.
    - 특정 노드에 접근하기까지 비용과 시간이 매우 크다.
- 장점
    - 새로운 노드의 추가, 삽입, 삭제가 쉽고 빠르다.
    - 현재 노드의 다음 노드를 얻어오는 연산에 대해선 비용이 발생하지 않는다.

## 이중 연결 리스트

- 연결 리스트의 탐색 기능을 개선한 자료구조로, 기존 연결 리스트는 헤드에서 테일 방향으로만 탐색이 진행 가능했지만 이중 연결 리스트는 포인터 변수를 두 개 가지고 양방향으로 탐색이 가능한 자료구조이다.
- 노드를 도중에 추가할 때, 이전 노드의 다음 노드 주소와, 다음 노드의 이전 노드 주소를 모두 수정해주어야 한다.

## 환형 연결 리스트

- 테일의 다음 노드가 헤드를 가리키고, 헤드의 이전 노드가 테일을 가리키는 원형 형태의 연결 리스트이다.
- 즉 테일에 접근하는 비용이 거의 없는 것이나 다름없을 정도로 끝을 빠르게 알 수 있어서 끝 노드에 새로운 노드를 추가하는 연산의 비용이 획기적으로 개선된다.
- 기존의 노드 추가 연산은 테일로 이동하여 추가하는 것이지만, 환형 연결 리스트에선 테일과 헤드 사이에 새 노드를 삽입하면 그만이다.

<aside>
💡

1. 삽입, 삭제, 탐색을 연결 리스트와 배열의 성능을 비교하며 설명하시오.

연결 리스트는 삽입과 삭제가 매우 빠르고 쉽지만, 탐색이 모든 노드를 순회해야 해서 느리다.
그러나 배열은 탐색이 매우 빠르지만, 삽입과 삭제가 뒤의 모든 데이터를 하나씩 옮기는 작업이 필요해서 느리다.
</aside>

<aside>
💡

1. 환형 링크드 리스트가 아닌 링크드 리스트에서 테일 노드의 위치를 바로 아는 방법은?

리스트의 구조체에 테일 노드의 주소를 저장하는 변수 하나를 추가해서,
테일 노드가 변경될 때마다 해당 변수에 테일 노드의 주소를 저장한다.
</aside>

<aside>
💡

1. 노드의 개수를 알아낼 때 N개의 노드를 전부 순환하는 것이 아닌, 즉시 크기를 알아내는 방법은?

리스트의 구조체에 size 변수를 하나 두고, 노드를 삽입할 때 size를 증가, 삭제할 때 감소시키면 된다. 이 경우 해당 리스트가 비어있는지도 size를 통해 바로 알 수 있다.
</aside>

<aside>
🎮

단일 연결 리스트는 비슷한 오브젝트간의 연결을 구현할 때 주로 사용된다. 빠른 삽입과 삭제가 가능하지만, 탐색은 할 필요가 없는 예시 중에선 오브젝트 풀링이 있다.
슈팅 게임 등에서의 모든 탄막 오브젝트를 연결 리스트에 담아 맵 밖으로 나간 탄막은 삭제, 새로 추가된 탄막은 삽입하여 모든 오브젝트를 쉽게 관리하는 감독 자료구조로 사용할 수 있다.
또한 삭제된 탄막은 삭제가 아닌, 테일에 새로 삽입하는 방식으로 풀링을 할 수도 있다.
거기에 더불어 순차적으로 진행되는 연계 퀘스트를 저장해두는 자료구조에서도 연결 리스트를 사용할 수 있다.

이중 연결 리스트는 양방향으로 탐색이 가능하기 때문에, 좌우로 이동하는 모든 인터페이스에서 유용하게 사용되는데, 특히 인벤토리나 UI 메뉴 시스템에서 주로 사용된다.

환형 연결 리스트는 턴제 게임의 페이즈에서 정말 자주 사용된다. 특히 TCG게임 장르의 경우, 드로우 페이즈, 메인 페이즈, 배틀 페이즈, 엔드 페이즈 등 자신의 턴이 끝나면 바로 다음 턴의 드로우 페이즈로 이동하는 경우가 있다. 지금이 어떤 턴인지 관리하는 부분에서 환형 연결 리스트를 사용하면 매우 편리하다.
또한 이중 연결 리스트와 마찬가지로, UI 메뉴에서 가장 오른쪽으로 도달하면 다시 왼쪽의 첫 번째로 갈 수 있는 방식으로도 사용 가능하다.

보통 게임에서 연결 리스트를 쓸 땐 이중 연결 리스트가 가장 많이 사용되는데, 이는 좌우로 이동해야 하는 인벤토리, 애니메이션 트랜지션, UI 메뉴 네비게이션 등에서 주로 사용되기 때문이다.

</aside>

# Chapter 02 : 스택

## 스택의 개념

- 스택은 반드시 꼭대기에서만 무언가 작업이 이루어지는 자료구조이며, 데이터의 추가, 반환, 입/출력 규칙이 스택의 끝부분에서만 이루어진다.
- 스택은 가장 마지막에 들어간 데이터가 먼저 나오고(LIFO), 가장 먼저 들어간 데이터가 나중에 나온다(FILO).
- 스택 ADT의 연산은 삽입(Push)과 삭제(Pop) 연산 두 가지 뿐이다. 그 외의 기능은 이 두 연산을 보조하는 기능에 지나지 않는다.

## 배열 기반 스택

- 배열 기반 스택은 구현이 간단하지만 용량을 동적으로 변경하는 비용이 크다는 단점이 있다.
- 배열 기반 스택은 구조체에 용량, 최상위 노드의 위치, 노드 배열 필드를 가지고 있어야 한다.
- 자유 저장소에 노드 배열을 할당하고, Nodes 필드가 자유 저장소 배열의 첫 번째 요소를 가리킨다.
- Capacity 변수는 해당 스택이 담을 수 있는 총 용량 변수이다.
- Top 변수는 초기엔 -1로 초기화한다. 첨자가 0인 것은 첫 번째 주소를 가리키는 형태이기 때문이다.
- 노드 삽입 연산은 Top에 1을 더한 곳에 새 노드를 입력하도록 구현하며, 제거는 Top 노드를 하나 낮추면 된다.

## 링크드 리스트 기반 스택

- 배열 기반 스택보다 좋은 점은 용량에 제한을 두지 않아도 된다는 것이다.
- 단, 배열과 달리 인덱스를 활용해 노드에 접근할 수 없으므로, 링크드 리스트는 자신의 위에 위치하느 노드에 대한 포인터를 갖고 있어야 한다.
- 즉 Capacity 변수는 필요가 없으며, Top 포인터만 가지고 있으면 된다. Top 포인터도 솔직히 필요는 없으나, 매번 삽입과 삭제를 할 때 마지막 데이터까지 순차 탐색을 해야하기에 보유하는 편이 훨씬 성능이 좋다.

## 스택을 활용한 사칙 연산

- 우리는 중위 표기법을 사용해 식을 계산하지만, 컴퓨터는 스택으로 사칙연산을 계산하기 때문에, 후위 표기법을 사용하여 (연산자가 숫자의 뒤에 오는) 계산한다.
- 이렇게 되면 숫자를 스택에 담고, 스택에 담겨있는 숫자를 바탕으로 새로 입력된 것이 연산자라면 해당 연산자를 통해 위 두 개의 숫자를 연산하고 그걸 최상위 노드로 대체하면 된다.
- 또한 중위 표기식을 후위 표기식으로 변환하는 알고리즘도 존재한다.
    - 토큰을 읽고 피연산자라면 토큰을 결과에 출력, 연산자라면 최상위 노드에 담긴 연산자보다 우선순위가 높은지 검사한 후, 출력하거나 스택에 삽입한다.

<aside>
🎮

스택 ADT는 게임에서나 소프트웨어 (특히 앱) 에서나 비슷하게 자주 사용되는데, 가장 많이 사용되는 곳은 팝업 UI이다. 특히 뒤로가기가 존재하는 네비게이션에서 스택이 정말 빈번하게 사용된다.

메인 화면 → 인벤토리 → 아이템 상세보기

순서대로 UI를 열었다면, 뒤로가기 또한 존재할 것이다.
위 UI 토큰을 전부 스택에 삽입하고, 뒤로가기를 수행하면 이전 스택에 있던 UI가 열리면 된다.
이렇게 할 경우, 팝업을 미리 다 띄어놓는 것보다 더 좋은 이점이 존재한다.

만약 아이템 상세보기에서 아이템을 판매하는 기능이 있을 경우,
UI를 끄지 않고 미리 다 띄워둔다면, 아이템 상세 보기에서 판매 후 UI를 닫았을 때 해당 정보가 인벤토리에 바로 갱신되도록 로직을 추가하는것이 복잡하다.

그러나 스택을 통해 UI를 그때마다 새로 띄우게 한다면, UI가 나타날 때 메모리를 참고하여 UI를 갱신하도록 한다면 상세 보기에 판매나 구입, 버리기를 넣었을 때 추가로 로직을 작성할 필요가 아예 없게 된다.

또한 턴제 게임에서 뒤로가기 기능에도 매우 좋다. 특히 시간을 뒤로 되돌리는 연출과 기능을 넣고 싶다면 스택은 반드시 필수불가결한 자료구조가 된다.

</aside>

# Chapter 03 : 큐

## 큐의 개념

- 큐는 스택과 반대로 먼저 들어간 데이터가 먼저 나오는 (FIFO) 선입 선출 ADT 자료구조이다.
- 줄을 세우고, 공평하게 처리하는 작업에서 반드시 사용되는 자료구조이다.
- 스택과 마찬가지로 삽입과 제거 연산이 있으며, 앞을 전단, 뒤를 후단이라고 부른다.
    - 즉 삽입을 하면 후단이 갱신되고, 삭제를 하면 전단이 갱신된다.

## 순환 큐

- 배열을 통해 큐를 만들게 되면 삭제 연산 시 모든 뒤의 요소들을 앞으로 옮기는 작업이 필요한데, 이는 연산적으로 굉장히 손해이다.
- 따라서 배열의 요소를 이동하지 않는 것이 좋은데, 이 때 동그랗게 순환 큐를 만들면 요소를 옮길 이유가 없어진다.
- 단 순환 큐는 공백 상태와 포화 상태가 존재한다. 두 상태 모두 일반적으론 전단과 후단이 겹친다.
    - 포화 상태에서의 전단과 후단 겹침을 해결하기 위해 더미 노드를 하나 따로 만들어서 전단과 후단이 겹치는 일을 방지한다.
- 현재 전단과 후단의 주소를 통해 공백 상태인지, 포화 상태인지 알 수 있다.
    - 후단과 전단의 차 (Rear - Front)가 큐의 용량(Capacity)와 동일하면 순환 큐는 포화 상태라고 할 수 있다.

## 링크드 큐

- 큐의 각 노드는 이전 노드에 대한 포인터를 이용해 연결된다.
- 전단 노드가 다음 노드의 포인터를 포함하고 있고, 이것이 연속되는 방식이다.
- 각 노드마다 포인터 변수를 가지고 있어야 하지만, 이론적으로 용량 제한이 없어 가득 차다는 개념이 없기 때문에 포화 상태를 확인하는 함수 자체가 필요 없다.
    - 물론 순환 큐는 malloc이나 free로 새 노드를 선언할 필요가 없어 순환 큐가 더 빠르다.

<aside>
💡

1. 전단과 후단의 위치가 동일하면 공백 상태이다.
포화 상태일 경우 더미 노드 덕분에 위치가 겹치지 않는다.
</aside>

<aside>
💡

1. 순환 큐는 구현은 어려우나 성능이 속도 면에서 좋다.
링크드 큐는 구현이 쉽고 용량 면에서 성능이 좋으나 속도 면에서 비교적 순환 큐보다 성능이 떨어진다.
</aside>

<aside>
🎮

큐 ADT는 플레이어의 선택이 엔딩에 영향을 주는 게임에서 컷신이나 이벤트 구조를 짤 때 쓰인다.

플레이어가 특정 스토리를 진행하며,
선택적으로 특정 NPC를 구하거나, 죽이거나,
선택적으로 특정 아이템을 먹거나, 먹지 않거나,
선택적으로 특정 이벤트를 발생시켰거나, 아니거나에 따라

엔딩 컷신에서 에필로그를 보여주는 컷신이 보여질지 안보여질지를 결정할 뿐더러
나와야 하는 대사 등을 조절할 수 있다.

배열에 모든 컷신과 대사를 전부 넣어놓고 이벤트를 수행했는가 아닌가에 따라 해당 요소만 스킵을 하는 방법도 존재하나, 큐를 활용하면 게임 도중에 이벤트가 몇 번 발생하고 다시 다음 게임이 시작되는 등 동적인 게임 흐름을 구현할 때 매우 유용하다.

</aside>

# Chapter 04 : 트리

## 트리의 개념

- 트리는 나무를 닮은 자료 구조로, 한 노드가 또 다른 노드 하나뿐만이 아닌 여러 노드와 연결이 가능한 노드이다. 그러나 루트 노드는 하나이다.
- 이는 게임에서의 예시를 굳이 찾을 필요도 없이 너무나 당연하게 많이 쓰이는 곳이 있는데, 바로 게임의 스킬 트리이다. 애초에 이름 부터가 트리이다.
- 컴퓨터 구조에서도 자주 쓰인다. 디렉토리 구조나 DOM도 트리 구조로 이루어져있다.
- 트리는 뿌리, 가지, 잎 세 가지 요소로 이루어져 있다.
    - 뿌리(Root)는 트리의 최상위 노드이며, 가지는 뿌리와 잎 사이에 있는 모든 노드를 일컫는다.
    - 마지막으로 가지 끝에 매달린 노드를 잎이라고도 하고, 단말(Terminal) 노드라고도 한다.
- 각 노드는 부모 노드를 가지고 있으며, 자신과 계열이 같은 형제 노드도 존재할 수 있다.
- 특정 노드부터 아래까지의 경로가 존재하며, 경로에는 길이라는 속성이 존재한다.
    - 뿌리 노드의 깊이는 0이다.
- 비슷한 용어로 레벨과 높이가 있다. 레벨은 깊이가 같은 노드의 집합을 일컫고, 높이는 가장 깊은 곳까지의 길이를 뜻한다.
- 차수(Degree)는 한 노드의 자식 개수인데, 트리의 차수라고 한다면 해당 노드의 노드들 가운데 가장 자식이 많은 노드의 차수를 의미한다.
- 트리는 다양한 방법으로 표현할 수 있다.
    - 소프트웨어의 GUI로도 표현할 수 있다.
    - 괄호로도 표현할 수 있다. 특정 노드의 자식을 괄호 안에 적으면 된다.
    - 중첩 집합으로도 트리를 표현할 수 있다. 루트 노드가 가장 바깥의 집합이다.
    - 들여쓰기로도 트리를 표현할 수 있다.
- 노드 또한 여러가지 방법으로 표현할 수 있다. 왼쪽 자식 - 오른쪽 형제 표현법을 통해 한 네모의 그림에 자식의 노드들을 적을 수 있다.

## 이진 트리

- 하나의 노드가 자식 노드를 2개까지만 가질 수 있는 트리이다.
    - 즉, 노드의 차수 값의 최대가 2이다.
- 이 트리를 활용해 정렬된 배열에서 빠르게 원하는 값을 찾을 수 있는 이진 탐색을 할 수 있다.
- 모든 노드가 두 개의 자식을 잘 가지고 있는 것을 포화 이진 트리라고 한다.
    - 완전 이진 트리는 왼쪽부터 차곡차곡 자식이 생기는 타입이다.
- 굳이 포화 이진 트리, 완전 이진 트리를 따로 정한 이유는, 이진 트리는 완전한 모습을 유지해야 높은 성능을 내기 때문이다. 완전히 극단적으로 나쁜 트리인 ‘편향 트리’는 사실상 배열과 다를 것이 없고, 이를 위해 스스로 밸런스를 잡는 트리들이 존재한다.
- 높이 균형 트리는 왼쪽 하위 트리와 오른쪽 하위 트리의 높이가 2 이상 차이나지 않는 트리이다.
- 완전 높이 균형 트리는 왼쪽 하위 트리와 오른쪽 하위 트리가 높이가 같은 트리이다.
- 이진 트리는 전위 순회, 중위 순회, 후위 순회 방식으로 순회할 수 있다.
    - ROOT의 위치에 따라 전위, 중위, 후위가 결정된다.
    - 전위 순회를 사용해 이진 트리를 중첩된 괄호로 표현할 수 있다.
    - 중위 순회를 사용해 수식 트리를 구현하고 표현할 수 있다.
    - 후위 순회를 사용해 후위 표기식을 구현하고 표현할 수 있다.

## 수식 트리

- 피연산자는 잎 노드이며, 연산자는 뿌리 노드 또는 가지 노드인 트리이다.
- 즉 가장 바닥에는 숫자가, 그 위 모든 노드에는 연산자가 들어간 이진 트리이다.
- 수식 트리를 올바르게 순회하는 방법은 후위 순회이다.
    - 즉 수식 트리를 구축할 때도 후위 표기식을 이용해 트리를 구축하는 것이 좋다.

## 분리 집합

- 집합의 정의는 특정 조건에 맞는 원소의 모임이다.
- 분리 집합은 서로 공통된 원소를 갖지 않는, 즉 교집합을 갖지 않는 복수의 집합을 뜻한다.
- 분리 집합은 데이터베이스 등에서 정가, 할인가 같은 별도의 속성이 필요할 때 쓰인다.
- 이를 트리로 표현할 수 있다. 이럴 경우, 서로 이어지지 않는 두 개 이상의 트리가 된다.
- 이렇게 분리 집합으로 구성된 트리에는 합집합 연산과 집합 탐색 연산이 있다.
    - 합집합 연산 시 하나의 트리를 다른 트리의 루트 노드에 연결해버린다.
    - 집합 탐색 연산은, 집합에서 원소를 찾는것이 아닌, 원소가 속한 집합을 찾는 연산이다.

<aside>
🎮

트리는 게임에서 아주 유용하게 쓰이는 자료구조이다.

가장 먼저 스킬 트리가 있다.
또한 비주얼 노벨에서 대사 선택지에 따른 응답 또한 트리로 만들며,
이는 더 나아가 퀘스트, 더 크게 이벤트 분기 또한 트리로 구현할 수 있다.

또한 AI의 행동 패턴을 책임지는 Behavior Tree를 구현하기 위해서도 필수적으로 사용된다.

</aside>