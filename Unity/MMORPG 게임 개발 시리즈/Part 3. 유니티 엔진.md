# 1. 개론

## 환경 설정

- 기본 3D 템플릿으로 프로젝트 생성
- 마음에 드는 Layout으로 설정
- 빈 오브젝트를 생성한 후 원하는 C# 스크립트를 생성해 컴포넌트로 부착

```csharp
void Start()
{
	Debug.Log("Hello Unity");
}
```

- 하이어라키에 변화가 생겼다면 씬 오른쪽에 *(별표)가 뜨므로, Ctrl + S로 씬 저장

# 2. 유니티 기초

## 에디터 입문

- Scene은 영화 세트장을 꾸미는 공간이다.
    - Alt를 누른 채 드래그하면 선택한 오브젝트를 중심으로 둘러보기가 된다.
- Game은 영화를 촬영하는 카메라의 미리보기 공간이다.
- Hierarchy는 계층 구조로 영화 세트장에 있는 사물과 배우를 표현하는 창이다.
    - 안에 있는 Camera는 영화 촬영 카메라, Light는 조명이다.
    - 현재 Scene에서 보고있는 구도로 Camera를 이동시키고 싶다면, Ctrl + Shift + F를 누르면 현재 구도 그대로 카메라가 이동하게 된다.
- Project는 영화에 사용할 배우 및 소품 창고이다.
    - Assets는 모든 영화 내 소품들이 들어가있는 폴더이다.
- Inspector는 영화에 사용되는 배우 및 소품의 정보 및 편집창이다.

## Play

- Ctrl + P 키를 통해 게임 플레이 가능
- Play 이후에 여러가지 사항들을 편집할 수 있음
    - 그러나 편집한 내용들은 실제로 반영되지 않음

## Component 패턴

- 유명한 디자인패턴 중 하나인 컴포넌트 패턴이 유니티 엔진이 돌아가는 원리의 핵심이다.
    - 언리얼 엔진은 상속과 컴포넌트 패턴을 블렌딩한 구조이다.
- 모든 코드를 전부 부품화하여, 재사용이 가능하게끔 오브젝트에 부착하는 패턴이다.
- 기본으로 제공하는 프리셋 오브젝트나, 내가 Create Empty로 만들어서 똑같은 컴포넌트를 부착시키는거나 다른 것이 전혀 없다.
- 직접 Monobehaviour 스크립트를 작성해서 컴포넌트를 부착하는 것이 코딩하는 방식이다.

## 매니저 만들기

- 우선, 컴포넌트로 쓸 C# 스크립트와, 컴포넌트로 쓰지않을 일반 C# 스크립트를 구분하자.
- MonoBehaviour를 상속받은 클래스는 new로 생성하는것은 매우 좋지 않다.
- MonoBehaviour는 Behaviour를 상속받고, 또 이는 Component를 상속받고, 이는 Object를 상속받는다.
- MonoBehaviour가 있어야만 이 클래스가 컴포넌트로 인식될 수 있다.
- MonoBehaviour를 상속 해제하면 일반 C# 스크립트처럼 사용할 수 있다.
    - 물론 이러면 Unity에서 제공하는 함수는 사용하지 못한다.

## Singleton 패턴

- Visual Studio에서 브레이크포인트를 찍고, Unity와 연결을 통해 디버깅을 할 수 있다.
- 다른 게임 오브젝트를 가져오는 방법은 너무나 많다.

```csharp
GameObject go = GameObject.Find("@Managers");
Managers mg = go.GetComponent<Managers>();
```

- 그러나 Find()는 오브젝트가 많을 수록 부하가 심하기에 거의 사용하지 않는다.
- 따라서 싱글톤 패턴을 사용해서 유일한 매니저를 보장함과 동시에 쉽게 인스턴스를 가져오게끔 할 수 있다.
- 기본적으로 우리가 일차적으로 생각할 수 있는, 유일 인스턴스 보장은 아래와 같다.

```csharp
public class Managers : MonoBehaviour
{
    static Managers Instance;
    public static Managers GetInstance() { return Instance; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
```

- static 전역 변수를 만들어서 어디서든 접근 가능하게 하고, 인스턴스를 return하는 것이다.
- 그러나 이렇게 구현하면 만약 GamaManager가 여러 개 존재할 때, 서로가 자기가 왕이라며 하나의 static 변수에 자신의 게임 매니저를 덮어씌우는 일이 발생한다.

```csharp
void Start()
{
    GameObject go = GameObject.Find("@Managers");
    Instance = go.GetComponent<Managers>();
}

Managers mg = Managers.GetInstance();
```
- Start를 이렇게 만들어두면, 여러개가 있더라도 하나만 인정하게 되기는 한다.
- 그러나 이 경우에도 Managers라는 오브젝트가 없다면 할당되지 않는다.
    - 만약 오브젝트가 없을 때 인스턴스를 가져오려고 한다면 null이 가져와진다.
    - 즉, 가져올 때 null이라면 새로 인스턴스를 할당시켜야 한다.

```csharp
static void Init()
{
    if (Instance == null)
    {
        GameObject go = new GameObject { name = "@Managers" };

        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }
        DontDestroyOnLoad(go);
        Instance = go.AddComponent<Managers>();
    }
}
```
- 이렇게 만든다면 게임오브젝트가 없더라도 새로 오브젝트를 생성한다.
- DontDestroyOnLoad를 통해 게임 오브젝트가 절대 제거되지 않도록 한다.
- 이 또한 개선점이 필요하다. 가져올 때 메서드가 아닌 프로퍼티로 가져오면 가독성과 성능이 좋아진다.

```csharp
static Managers s_instance;
public static Managers Instance { get { Init(); return s_instance; } }

// Start is called before the first frame update
void Start()
{
    Init();
}

// Update is called once per frame
void Update()
{
    
}

static void Init()
{
    if (s_instance == null)
    {
        GameObject go = new GameObject { name = "@Managers" };

        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }
        DontDestroyOnLoad(go);
        s_instance = go.AddComponent<Managers>();
    }
}
```

```csharp
Managers mg = Managers.Instance;
```

- 물론 이 형태가 정석적인 싱글톤 패턴의 형태는 아니나 기본적인 개념은 유사하다.

## 플레이어 설정

- C# 스크립트에서 기본적으로 뜨는 주석이 아예 없게끔 생성하기 위해선 별도 설정이 필요하다.
    - 에디터 폴더에 들어가서 스크립트 템플릿 폴더로 들어간다.
    - 여기서 81번을 열어보면 기본 템플릿이 있고, 이걸 수정하면 된다.
    - 권한 문제가 있기 때문에 바로 수정하기보단 복사 후 수정한다음 덮어쓰기하는 것이 좋다.
- 에셋스토어에서 에셋을 다운받아 플레이어를 제작함.
    - 유니티짱 기본 에셋 다운로드
    - Scene, Scripts, Art, Prefabs 등은 임포트할 필요 없음
- 모델 FBX와 머티리얼등이 존재한다. FBX파일은 기본적인 조각상 형태이고, 여기에 머티리얼이 덮어씌워져 모델이 만들어진다.
    - 모델에 맞는 텍스처를 만들었다면, 그 모델에 적합하도록 바꾸는 것이 머티리얼이다.
    - 머티리얼은 재질이기 때문에 빛 반사율 등이 재질에 영향을 준다.
- 또 애니메이터들이 만들어놓은 애니메이션 클립을 사용해 애니메이팅 효과를 줄 수 있다.
- 기본 Scene의 설정은 Shaded이지만, 와이어프레임 등으로 씬을 전환하면 폴리곤을 볼 수도 있다.
- 새로 PlayerController이라는 이름의 플레이어 스크립트를 생성한다.
    - 키보드 입력에 따른 이동을 구현한다.
- 보통 같은 오브젝트 안에 있는 컴포넌트를 가져올 땐 GetComponent를 사용해야하지만, 기본적으로 모든 오브젝트에 들어있는 트랜스폼을 조작할 땐 그런 작업 필요없이 transform만 사용해도 된다.
    - 포지션에 새로운 벡터를 더하여 이동을 구현할 수 있다.

## Position

- update 문에 바로 이동을 바로 박아버리면 매 프레임마다 이동하기 때문에 너무 빠르게 이동한다.
- 따라서 이런 경우엔 Time.deltaTime을 사용해야 한다.
    - deltaTime은 한 프레임이 완료될 때까지 걸린 시간이다. 이걸 곱하면 내가 원하는 좌표까지 이동하는 데에 어떤 프레임을 가진 하드웨어라도 동일한 크기만큼 이동하게 된다.
- 변수를 인스펙터에 꺼내서 고쳐쓰고 싶다면 public을 쓰거나 [SerializeField]를 쓸 수 있다.
    - SerializeField의 대괄호 방식은 C#의 문법인 리플렉션이다.
- 이를 직렬화라고 하고, 오브젝트나 프리팹 등도 할당시킬 수 있다.

```csharp
transform.position += new Vector3(0, 0, 1) * Time.deltaTime * _speed;
transform.position += Vector3.forward * Time.deltaTime * _speed; // 리팩토링
```

- 단순한 크기 1의 방향 벡터를 원하는거라면 벡터를 직접 만들 필요 없이 이미 예약된 키워드를 쓸 수 있다.
- Rotation을 회전시킨 후 위 이동 메서드를 사용하면 돌아가있는 채 이동하는 것을 볼 수 있는데, 이는 이동 방식이 상대 좌표 방식이 아닌 **월드 좌표계 방식**이기 때문이다.
- 유니티 Scene에서 X키를 누르면 해당 오브젝트 기준 좌표계를 볼 수 있다.

```csharp
transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
```

- transform.TransformDirection() 메서드를 사용해 로컬에서 월드로 좌표 계산을 해줄 수 있다.
- 파라미터 안 방향을 월드 기준으로 해석하면 어디인가? 를 계산해주는 메서드이다.
    - 움직이고 싶은 방향을 로컬 기준으로 정했기 때문에, 그걸 월드 좌표계상에서 얼마인지 계산해주는 것이다.

| Local → World | World → Local |
| --- | --- |
| TransformDirection | InverseTransformDirection |

```csharp
transform.Translate(Vector3.forward * Time.deltaTime * _speed);
```

- 혹은 이렇게 position에 더하는 것이 아닌, Translate 메서드를 쓰면 바로 로컬 좌표로 이동하게 해주는 메서드 자체가 별도로 존재한다.

## Vector3

- position은 좌표인 줄 알았는데 벡터가 더해지고, 벡터와 상수값이 곱해지기도 한다.
- 이렇게 궁금한 게 있다면 F12를 눌러 직접 열어볼 수 있다.
- Vector3를 열어본다면 public으로 x, y, z가 존재한다는 것을 알 수 있다.
- 게임 엔진에서의 Vector는 수학에서의 벡터와 동일하며, **위치 벡터와 방향 벡터**가 존재할 수 있다.
    - 위치 벡터는 position 등에서 사용하는, x와 y와 z의 위치를 나타내는 벡터이다. 덧셈 등을 할 수 있다.
    - 방향 벡터는 위치랑은 상관 없고, 현재 수치에서 방향을 정의하는 벡터이다. forward 등이 그 예시다.
        - A지점까지 가야하는 B지점의 방향 벡터를 구하고 싶다면, A-B를 수행하면 된다. 이 방향 벡터는 단순히 방향뿐만 아니라 크기 또한 가지고 있다. 목적지에서 나를 빼야 한다.
        - 방향 벡터에서 얻을 수 있는 두 가지 정보는, 거리(크기)와 실제 방향이다.
- 방향 벡터의 정보를 추출하는건 직접 하지 않아도, 당연히 엔진에 기본 메서드가 존재한다.

```csharp
transform.position.magnitude // 거리(크기)를 반환하므로 float 리턴
transform.position.normalize // 크기가 1인 단위 벡터를 리턴
```

- 크기를 구하기 위해선 모든 좌표를 제곱하고 더한 피타고라스의 정리를 사용한다.
- 단위 벡터를 구할 땐 자기 자신의 좌표값들을 모두 크기로 나누어주면 크기가 1인 벡터가 된다.
- 이렇게 Vector3 자료형이 알아서 operator 을 직접 설정해두었기 때문에 우리는 벡터에도 상수를 곱할 수 있다.

## Input Manager (옵저버 패턴)

- update 문 안에서 입출력장치의 감지를 if문으로 다 체크해버리면 성능적으로도, 가독성적으로도 매우 좋지 않을 것이다.
- 즉 인풋에 대해서는 특정 매니저에서 감지한 후 이벤트를 건너받아 처리하면 훨씬 깔끔해지게 될 것이다.

```csharp
using System;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }
}
```

- 우선 단순 C#을 사용하여 Action이라는 이미 주어진 델리게이트를 사용한다.
    - 델리게이트의 문법 특성상, 여기에 원하는 함수를 삽입할 수 있게 된다.
- OnUpdate라는 메서드를 만들어서 무언가 입력되고 있는게 있는지 체크하고, 있다면 Invoke()를 통해 자신을 구독하는 객체들에게 이벤트를 뿌린다. 즉, 옵저버 패턴을 구현한다.

```csharp
InputManager _input = new InputManager();
public static InputManager Input { get { return Instance._input; } }

void Update()
{
    _input.OnUpdate();
}
```

- 싱글턴 패턴으로 구현한 게임 매니저에게 인풋 매니저 객체를 하나 만들어서 가져오게끔 하고, 매 프레임마다 인풋 매니저의 OnUpdate를 실행한다.

```csharp
void Start()
{
    Managers.Input.KeyAction -= OnKeyboard; // 기존에 등록된 이벤트가 있을 수도 있으니 제거
    Managers.Input.KeyAction += OnKeyboard; // 이벤트에 내 함수 등록
}

void OnKeyboard()
{
		// 이동 관련 로직
}
```

- 플레이어 컨트롤러 스크립트에 가서, 게임매니저가 생성한 인풋 매니저 객체의 KeyAction 델리게이트에 따로 빼둔 이동 관련 로직을 구독시킨다.
- 구독 전에 한 번 뺌으로서 이중 구독을 방지할 수 있다.
- 이 구조로 만들면 물론 매 프레임마다 update에서 체크하는 것은 똑같지만, 이제 인풋을 받는 모든 객체가 하나같이 update문에서 입력을 체크받을 필요가 사라진다. update문을 돌리며 인풋을 감지하고, 이벤트를 뿌리는 것은 게임 매니저의 인풋 매니저 객체가 유일하게 일을 하게 되기 때문이다.
- 또한 특정 입력을 받았을 때 누가 이 구독을 신청했길래 이벤트가 발생하는지 궁금하다면 인풋 매니저의 Invoke()에서 브레이크 포인트를 잡고 F11을 눌러 다음 프레임으로 실행해보면, 어떤것이 실행되는지 디버깅도 비교적 쉽게 할 수 있다.

# 4. Prefab (프리팹)

## Prefab

- 부모 오브젝트에 물린 자식 오브젝트는, 부모 오브젝트의 크기나 위치가 변한다고 Transform이 변하지 않는다. 부모 기준에서의 오브젝트 위치를 뜻하기 때문이다.
- 이렇게 만든 오브젝트를 똑같은 것이 여러 번 나올 때, 복사 붙여넣기로 몇 백개 만드는 것은 말이 안된다. 이 청사진을 따로 저장할 수 있는 것이 프리팹이다.
- C#에서 클래스를 만들고, 그걸로 인스턴스화를 하는 것과 거의 똑같다.
- 프리팹 오브젝트가 여러 개 있을 때, 원본만 수정해도 모든 오브젝트가 일괄적으로 모두 변경되기 때문에 조정이 매우 쉽다는 장점이 있다.
    - 프리팹을 수정하고 싶다면 프리팹을 더블클릭해 프리팹 모드로 들어가 수정하면 된다. 오토 세이브 기능이 있기 때문에 수정하는 즉시 저장된다.
- 가끔 프리팹마다 다른 속도나, 다른 값을 가지고 싶을 때가 있다. 프리팹으로 생성한 오브젝트에서 인스펙트의 값을 바꾸는 것을 오버라이드라고 한다.
    - 오버라이드한 값이 있다면, 프리팹 원본을 수정해도 우선적으로 오버라이드한 값이 유지된다.

### Nested Prefab

- 프리팹 안에 프리팹이 포함되어있는 관계
- 1개 이상의 프리팹을 자손으로 두고있는 프리팹
- 네스티드 프리팹 내 프리팹 역시 오버라이드되어있다면 원본값의 변화에 반영이 안된다.

### Prefab Variant

- 객체지향의 상속과 매우 유사한 개념
- 특정 프리팹의 모든 기능을 따르되, 추가적인 정보를 가지고 있는 프리팹
- 이미 프리팹인 오브젝트를 한 번 더 에셋으로 드래그하면 알림창이 뜨는데, 이 때 프리팹 바리언트를 선택할 수 있다.
    - 아이콘의 한 면이 회색으로 변한 것을 볼 수 있다.

## Resource Manager

- 프리팹 역시 코드 상에서 생성할 수 있다.

```csharp
Instantiate(prefabObject); // 프리팹 인스턴스화
Destroy(prefabObject); // 오브젝트 제거
```

- 프리팹을 담기 위한 자료형은 GameObject이다.
- 오브젝트를 할당하는 것 역시 인스펙터에서 할 수 있지만, 코드상으로도 가능하다. 게임의 규모가 클 수록 코드상에서 오브젝트나 프리팹을 할당하는 것이 훨씬 좋다.

```csharp
prefabObject = Resources.Load<GameManager>("Prefabs/Tank");
```

- 이 때 중요한건, Assets 폴더의 아래 Resources라는 이름의 디렉터리에 있는 자료들만 가져올 수 있다.
- 그럼에도 모든 스크립트가 하나같이 이렇게 리소스를 가져오게 되면, 너무 많은 스크립트가 리소스를 덕지덕지 가져오기 때문에 이 또한 매니저가 있는 것이 조금 더 구조적으로 유리하다.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab at path: {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Attempted to destroy a null object.");
            return;
        }
        Object.Destroy(obj);
    }
}
```

- 리소스 매니저를 통해 오브젝트 가져오기, 인스턴스화, 오브젝트 제거를 만들 수 있다.
- 이 역시 게임 매니저에서 만들어둔 후 객체를 할당해둘 수 있다.

```csharp
Manager.Resource.Instantiate("Tank");
```

- 추후 사용할 땐 이렇게 적기만 해도 깔끔하게 정리할 수 있다.
- 이 역시 디버깅이 매우 좋고 코드가 깔끔해진다는 장점이 있다.

# 5. Collision (충돌)

## Collider

- 아무런 컴포넌트가 없으면 물리 적용을 받지 않는다.
- 물리 적용을 받기 위해선 리지드 바디가 필요하다.
    - Mass : 질량
- 또한 해당 물체의 물리적 상호작용 공간을 위해선 콜라이더가 필요하다.
    - 콜라이더는 쉽게 말해 충돌 범위이다.
    - 또한 콜라이더는 충돌하고자 하는 양 오브젝트에 모두 필요하다.