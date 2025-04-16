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

## Collision

- 리지드바디의 is Kinematic은 이 리지드바디가 물리 영향을 받을지 말지 체크하는 옵션이다.
    - 체크하게 되면 아예 물리 영향을 받지 않는다.
    - 즉 중력 영향 조차 받지 않으므로 Gravity 체크가 의미가 없어진다.
    - 굳이 리지드바디를 받아놓고 키네메틱을 켜는 이유는, 실제로 물리적인 적용은 없으나 충돌 시 판정 을 따지기 위함이다.
    - 예를 들어 엘리베이터를 만든다고 친다면, 콜라이더만 있고 리지드바디가 없을 땐 직접 position을 움직여야 하며, 이 땐 올라탄 플레이어가 떨리거나 튕기는 문제가 일어날 수 있지만, Kinematic Rigidbody를 사용하면 플레이어와의 충돌을 계산할 수 있다. 즉, 움직이는 오브젝트라면 리지드바디가 필요하다.
- 또한 리지드바디의 Freeze Rotation을 체크하면 더이상 회전값이 잠금되어 넘어지지 않는다.
- Collision 이벤트는 리지드바디가 있어야만 실행되므로 두 오브젝트 중 하나에는 필수적이다.
```csharp
private void OnCollisionEnter(Collision collision) { }
private void OnCollisionStay(Collision collision) { }
private void OnCollisionExit(Collision collision) { }
```

- 위의 OnCollision 메서드가 발동되기 위해서는 다음과 같은 조건이 필요하다.
    - 나 혹은 상대에게 리지드바디가 있어야 하며, 키네메틱은 Off
    - 나한테 콜라이더가 있어야 하며, 트리거는 Off
    - 상대한테 콜라이더가 있어야 하며, 트리거는 Off

```csharp
private void OnCollisionEnter(Collision collision) {
	collision.gameObject.name; // 이런 식으로 충돌한 대상의 정보를 가져올 수 있다.
}
```

## Trigger

- 물리와 전혀 상관 없이, 해당 충돌 판정 범위 안으로 들어왔냐, 얼마나 들어와 있냐 등을 판정하고자 할 때 쓰는 것이 Trigger Collider 옵션이다.
- is Trigger을 켠 이상 더 이상 충돌은 일어나지 않는다.
- 또한 둘 다 리지드바디가 없다면 Trigger 이벤트가 발생하지 않는다.

```csharp
private void OnTriggerEnter(Collision collision) { }
private void OnTriggerStay(Collision collision) { ]
private void OnTriggerExit(Collision collision) { ]
```

- 둘 다 콜라이더가 있어야 하며, 둘 중 하나는 Trigger모드고, 둘 중 하나는 리지드바디가 있어야 한다.
- 원하는 부분에 도달했을 때 이벤트가 발동되고 싶다면 매우 적절하다.

## Raycasting

- 3D 공간의 오브젝트를 2D 화면에서 마우스로 찍었는데, 3D 오브젝트가 선택되는 것은 굉장히 신기한 일이다.
- 스타크래프트같은 경우에도 3D 게임에서 마우스로 유닛을 클릭했을 때 특정 오브젝트가 선택된다.
- 2D 화면에서 특정 좌표 지점을 찍었을 때, 카메라 각도부터 오브젝트 선택까지 구현하는 것은 매우 힘들다.
- Raycasting은 말 그대로 레이저를 쏴서 먼저 부딪치는 오브젝트를 지정하는 기술이다.
- 마우스로 화면을 클릭하면 카메라 지점부터 3D 월드까지 레이저를 발사하는 것이다.

```csharp
Debug.DrawRay(transform.position, Vector3.forward * 10, Color.red);

RaycastHit hit;
bool isCasting = Physics.Raycast(transform.position, Vector3.forward, out hit, 10);
```

- 레이캐스트 메서드를 통해 실제로 레이캐스팅을 쏠 수 있다.
- Physics.Raycast는 방향벡터만 넣어도 알아서 무한히 쏘지만, 파라미터 버전에 따라 길이를 입력할 수도 있다.
    - out hit 파라미터를 통해 충돌한 오브젝트의 정보를 가져올 수 있다.
- 만약 바라보는 방향에 쏘고 싶다면, 아래와 같이 로컬 좌표를 써야 한다.

```csharp
Vector3 look = transform.TransformDirection(Vector3.forward);

Debug.DrawRay(transform.position, look * 10, Color.red);
```

- 일반 레이캐스팅은 먼저 닿은 오브젝트만 처리하지만, RaycastAll()을 사용하면 부딪친 모든 오브젝트들의 정보를 가져올 수 있다.
    - 이 경우 리턴하는 것이 RaycastHit 배열이다.
- 또한 플레이어와 카메라 사이에 물체가 있어서 플레이어를 가리는 경우, 플레이어를 투명하게 보이게 할 수도 있지만, 카메라로부터 플레이어까지 레이캐스팅을 쏴서 오브젝트가 있다면 그 앞으로 카메라가 이동하게끔 하면 할 수 있다.

## 투영

- 도대체 모니터를 찍으면 어떤 기준으로 좌표를 알고, 3D 오브젝트를 집게 되는가?
- 지금까지는 로컬 좌표계, 월드 좌표계에 대해 공부했다.
- 그러나 게임 세상에 있는 모든 오브젝트들의 배치는 3D인데, 실제로 우리 모니터에 그려지기 위해선 2D화면에 그래픽이 그려져야 한다.
    - 이를 위해선 Viewport ↔ Screen을 알아야 한다.

```csharp
Input.mousePosition // 현재 마우스 좌표를 픽셀 좌표로 뽑아온다.
```

- 스크린 좌표계
    - 화면상으로 가장 왼쪽 아래가 0, 0, 0 좌표가 된다.
    - 위로 올리면 Y좌표, 오른쪽으로 이동하면 X좌표가 커진다.
    - Z좌표는 항상 0으로 고정된다.
    - 즉, 게임이 그려지는 모니터에 있는 픽셀 좌표계이다.

```csharp
Camera.main.ScreenToViewportPoint(스크린 픽셀 좌표)
```

- 뷰포트 좌표계
    - 뷰포트는 스크린이 아닌 카메라와 상관이 있는 것이다.
    - X와 Y의 좌표값 범위가 0~1이다.
    - 즉 스크린 좌표계와 매우 유사하나, 실제 픽셀과는 상관 없이 화면에 대해 얼만큼의 비율을 차지하고 있는지를 나타내준다.
- 카메라의 Near는 카메라가 촬영을 시작하는 면이다.
- 반대로 카메라의 Far는 어디까지 촬영하는지를 적는 범위이다.
    - 카메라에 찍히지 않는 물체는 컬링이 되어 애초에 그려지지 않는다.
    - 바닥도 월드도 아무것도 그려지지 않으면 유니티 기본 공간인 회색 등이 화면에 그려진다.
- 또한 3D 세상에서는 원근법이라는 것이 존재해서, 멀리 있는 것은 더 작게 그려진다. 이것이 투영의 개념이다.
    - 이를 활용한 착시 게임이 슈퍼리미널이다.
- 투영을 하게 되면 3D상의 Z좌표 (깊이값) 하나가 아예 사라진다.
- 투명의 원리는, 아까 썼던 Far에 캡쳐된 것을 Near까지 축소시켜 캡쳐된 것을 그리는 것이다. 그렇기에 Near와 Far가 중요하다.
- 또 투영은 반드시 비율을 지킨다. 멀리 있었을 때 중앙에 있었다면 가까이에 와도 중앙에 있을 것이다.
- 우리가 클릭한 지점에서의 월드 좌표를 구해보자.

```csharp
Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vecor3(Input.mousePosition.x,
																														 Input.mousePosition.y,
																														 Camera.main.nearClipPlane));
																														 
Vector3 dir = mousePos - Camera.main.transform.position;
dir = dir.normalized;

RaycastHit hit;
if (Physics.Raycast(Camera.main.transform.position, dir, out hit))
{
	Debug.Log(hit.collider.gameObject.name);
}
```

- 위는 그래픽적인 원리를 풀기 위한 로직이고, 유니티에선 조금 더 쉽게 사용할 수 있다.
    - 그것은 Ray를 이용하는 것이다.

```csharp
Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

RaycastHit hit;
if(Physices.Raycast(ray, out hit))
{
	Debug.Log(hit.collider.gameObject.name);
}
```

## LayerMask

- 간단한 캡슐 콜라이더나 스페어 콜라이더는 연산이 쉽지만, Mesh 콜라이더를 쓰면 엄청 정교한 콜라이더가 사용되는 대신 연산량이 매우 많아진다.
    - 이는 게임 내에서 극히 예외적으로 사용해야 한다.
- 따라서 폴리곤이 매우 많은 오브젝트의 경우엔 폴리곤이 훨씬 적은 충돌 전용 매쉬를 만들고 중첩 배치를 하여 연산량을 줄일 수도 있다.
- 그럼에도 오브젝트가 너무 많으면 레이캐스팅을 할 때 연산량이 너무 많아지는 문제가 있다.
- 그래서, 연산하고자하는, 레이캐스팅하고자 하는 오브젝트를 별도로 구분할 수 있다.
    - 카메라가 플레이어를 가리지 않게 할 땐 환경에 관련된 것만. (NPC 등은 예외)
    - 화면으로 몬스터를 클릭할 땐 자연환경은 제외하고 플레이어나 몬스터 관련된 것만.
- 이는 오브젝트 인스펙터의 오른쪽 위에 Layer를 사용해서 설정할 수 있다.
    - 기본 유니티 개수는 0번부터 31번까지 총 32개이다.
- Add Layer를 눌러 원하는 레이어를 추가할 수 있다.
    - 몬스터 전용 레이어를 만들고, 플레이어 전용이나 벽 전용 등의 레이어를 만들 수 있다.

```csharp
Physices.Raycast(ray, out hit, int layerMask) // 파라미터 뒤에 레이어 마스크 비트플래그
```

- 여기서 중요한 것은 비트플래그를 사용하는 것이다.
- 모든 레이어중 적용을 원하는 레이어 번호만 1로 켜줘서 0 0 0 0 1 0 0 1 등으로 만들면 된다.
    - 이 2진수인 비트플래그를 10진수로 사용하면 된다.

```csharp
int mask = (1 << 8) | (1 << 9); // 8번 마스크와 9번 마스크에 비트 1을 시프트 연산
Physices.Raycast(ray, out hit, mask) // 혹은 768을 입력해도 되지만, 가독성은 시프트가 좋다
```

- 레이캐스팅은 부하가 많은 작업이기 때문에 이렇게 최적화 작업이 필수적이다.

```csharp
LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
```

- 위와 같은 방식으로 레이어 마스크 이름을 가져와 사용할 수도 있다.
- 이는 int형이 아님에도 저 파라미터에 넣을 수 있는데, 이는 LayerMask 자료형의 특징이다.
    - public int value를 가지고 있고, operator 연산도 지원하기 때문이다.

### Tag

- 태그는 오브젝트에 특정 칭호를 입히는 것과 마찬가지이다.
- 메인 카메라를 찾는 방식은 MainCamera 태그가 붙은걸 찾는 방식이기 때문에 카메라는 태그를 빼선 안된다.
- 이 외에도 직접 만든 오브젝트에 태그를 붙여서 FindGameObjectWithTag 등의 메서드를 사용할 수 있다.
    - Tag 역시 레이어마스크처럼 직접 만들 수 있다.

# 6. Camera (카메라)

## Camera

- 플레이어를 따라가는 카메라를 위해선 카메라 컨트롤러 스크립트가 별도로 필요하다.
    - 플레이어의 자식 오브젝트로 둬버리면 모든 로테이션을 다 따라가기 때문에 매우 어지럽고 거의 추천되지 않는다.
    - 플레이어의 회전값과 상관없이 플레이어와의 거리를 지켜야 한다.
- 컬링 마스크를 통해 촬영되지 않고자 하는 레이어 마스크 층도 지정할 수 있다.
- Target Texture를 통해 CCTV 등의 멀티 카메라도 만들 수 있다.
- Enum을 통해 카메라의 모드가 어떤 모드인지 정의할 수 있다.
- 카메라 모드와 델타 벡터값, 플레이어를 인스펙터로 빼서 별도로 정의할 수 있게 한다.
- Update() 문에서 플레이어의 포지션을 가져와 델타값을 조정하고, LookAt으로 플레이어를 지켜보게 한다.
    - 그러나 이렇게 Update()문에서 갱신을 해버리면 덜덜거리는 문제가 있는데, 이는 플레이어 물리 연산과 같은 Update문에서 실행되기 때문이다.
    - 둘 중 무엇이 먼저 실행될지는 랜덤이다.
- 따라서 이 때는 LateUpdate() 문에다가 카메라 로직을 넣는 것이 정답이다.
    - LateUpdate()문은 반드시 Update문 뒤에서 실행된다.

# 7. Animation (애니메이션)

## 애니메이션 기초

- 애니메이터들이 예쁘게 모델이 자연스럽게 움직이는 애니메이션 클립을 만들어준다.
- 애니메이션 클립의 인스펙터에서 애니메이션 탭 - 애니메이션 미리보기를 통해 동작을 확인할 수 있다.
    - 이 때 기본 휴머노이드 말고, 유니티가 지원하는 휴머노이드로 교체한다.
    - 혹은 애니메이션 창에 유니티짱 등의 3D 모델을 드래그해오면 적용된다.
    - 즉 애니메이션 클립은 사람 형태의 애니메이션이라면 어떤 인간 모델링이던 모두 적용 가능하고, 그런 타입을 휴머노이드라고 한다.
- 유니티짱 아바타를 켜보면 인체 모양의 구조를 확인할 수 있다.
    - 각 부위들을 애니메이션에 연결하는 것을 리타게팅이라고 한다.
    - 리타게팅을 할 수 있다면 공용 애니메이션을 가지고 다른 모델링들이 공유해 쓸 수 있다.
- 단순히 애니메이션 클립을 켜는 것 만으로는 애니메이션이 동작하지 않는다.
    - 단순 애니메이션 컴포넌트도 존재하는데, 이는 레거시 버전이라 요즘은 거의 사용하지 않는다.
- 애니메이션들을 컨트롤하기 위한 상태 패턴의 감독인 애니메이터가 필요하다.
    - 애니메이터 컴포넌트에 넣기 위한 가장 메인 파라미터로 컨트롤러라는 것이 필요하다.
    - 이는 유니티 고유의 애니메이팅 방식인 메카님 방식이다.
- 애니메이터의 Entry에서 출발하는 것이 출발 애니메이션 클립이다.
- 애니메이터의 각 애니메이션 클립끼리 연결 후 상태에 따라 변하도록 파라미터값을 조정해야 한다.

## 애니메이션 블렌딩

- 애니메이션 간 전환을 할 때 너무 뚝 끊기는 것은 게임에서 가장 지양해야 하는 연출이다.
- 그러기 위해선 두 애니메이션을 섞어야 한다.
- 애니메이터에는 블렌드 트리라는 것을 만들 수 있는 노드가 준비되어있다.
    - 블렌드 트리 노드를 클릭하면 해당 노드 내부로 들어가게 된다.
- 블렌드 트리 내에서 Add Motion을 통해 원하는 애니메이션을 넣고, 원하는 블렌딩 변수 파라미터를 설정한다.
    - Threshold 값이 0에서 1까지 진행하며 두 애니메이션 사이를 이동하게 된다.
- 값을 0과 1 사이를 조정해보면 아래 미리보기에서 애니메이션이 섞여있는것을 알 수 있다.
    - 즉 실제로 블렌딩하고 싶을 때 해당 파라미터 변수값을 조정해주면 된다.

## State 패턴

- 위에서처럼 수동으로 블렌딩하는 작업이 여간 귀찮은 일이 아니다.
- 애니메이션이 백개 이상 있을 때 그것에 대한 모든 블렌딩을 만드는 것이 매우 노가다이며, 코드가 깔끔하지도 않다.
- 특히 캐릭터의 상태가 여러 개 추가되면 그에 따른 if-else문이 너무나 많아진다.
- 즉 boolean으로 캐릭터의 상태를 관리하는 것은 말이 안되고, 이렇게 상태를 많이 사용하는 경우엔 반드시 상태 패턴을 사용하는 것이 좋다.
- 우선 Enum으로 상태별 자료형을 정의한다.
- 이후 플레이어의 상태를 표현하는 변수를 만들고, switch-case문으로 상태에 따른 분기점을 만든다.
    - 각 분기에 맞는 상태별 Update()문을 호출한다. 이러면 상태별로 업데이트문을 따로 관리하게 된다.
- 단, 단점이라고 한다면 동시에 두 가지 이상의 상태를 가질 수 있는 일은 없다.
    - 즉 움직이면서 스킬 캐스팅 등의 상태를 관리하려고 한다면 조금 어려워진다.
```csharp
void Update()
{
    switch (_state)
    {
        case PlayerState.Die:
            UpdateDie();
            break;
        case PlayerState.Moving:
            UpdateMoving();
            break;
        case PlayerState.Idle:
            UpdateIdle();
            break;
    }
}
```

- 단 고전적이고 정석적인 형태의 상태 패턴의 경우엔 Enum으로 분류하는 것이 아닌 상태별로 클래스가 존재하고, 현재 상태를 클래스로 관리하는 경우도 존재한다.

## State Machine

- 보통 상태 패턴을 만들면, 당연히 상태별로 대응되는 애니메이션도 존재할 것이다.
- 이렇게 상태에 따라 행동해야 하는 패턴들의 관계를 정의해놓고, 조건에 따라 상태를 전환하는 구를을 스테이트 머신 (상태 기계)라고 한다.
    - 유한 상태 기계(FSM)은 상태가 무한한게 아닌 정해진 개수의 상태만을 가진다는 것이며, 게임 개발에서 무한한 상태를 가지긴 어려우니까 사실 상태 기계 = 유한 상태 기계라고 해도 될 정도로 거의 비슷하다.
- 유니티 애니메이터 컨트롤러에서 상태 머신을 짜기 위해선 Make Transition으로 방향을 정해야 한다.
    - 트랜지션을 정하기만 하고 아무것도 하지 않으면, 서로 각 애니메이션이 종료될 때마다 다음 애니메이션으로 넘어가는 기본값을 가지게 된다.
- 유니티 애니메이터 컨트롤러의 인스펙터 세팅상 알아서 애니메이션 블렌딩을 해준다.
- HasExitTime은, 끝나는 지점에서 다음 애니메이션으로 전환하는 옵션이다.
    - 즉, 이 옵션을 끄면 무한하게 애니메이션을 반복한다.
    - 이 옵션을 끌것이라면 다른 상태로 이동하기 위한 파라미터 조건을 반드시 달아야한다.
- 원하는 노드에서 트랜지션 순서를 바꾸면, 조건이 없는 연결점에서 가장 위에 있는 노드로 이동한다.
- Settings 값을 바꾸면 그래프 블렌딩 값이나 시간 등을 조정할 수 있다.
- 경우에 따라선 애니메이션 전환 조건을 따로 넣지 않고 이렇게 자동 종료 후 이동을 넣어주는 것이 좋을 때가 있지만, 대부분 조건에 따라 전환되도록 설정해야 한다.
    - 그를 위해선 파라미터를 사용해야 한다.

```csharp
anim.SetFloat("speed", _speed); // 파라미터 설정
```

- 다만 액션 MMO 같은 경우엔 스킬이 굉장히 많고, 스킬 애니메이션이 굉장히 많아 그걸 하나하나의 노드로 전부 설정해버리기 시작하면 하드코딩과 같은 개념이 된다.
- 따라서 대부분 이동, 점프, 수영 등의 기본 움직임은 상태 머신에 넣고, 스킬은 따로 관리하는 경우가 조금 더 보편적이다.

## KeyFrame Animation

- 오브젝트의 행동이나 이동이 모두 이미 정해져있고, 그걸 계속 반복하는 애니메이션
- 게임에 종속적으로 행동하는 애니메이션들이고, 무언가 특정 객체에 전용적인 애니메이션을 키프레임 애니메이션이라고 한다.
- 원하는 오브젝트를 클릭한 채 애니메이션 창을 열면, 키프레임이 나타나며 애니메이션 클립을 직접 만들 수 있는 창이 나타난다.
- 여기서는 모델의 움직임뿐만 아니라 게임에서 사용하는 데이터인 이동값, 컴포넌트의 변수 등을 모두 설정할 수 있다. 한 마디로 게임에 대한 애니메이션을 지정할 수 있는 것이다.
- 변경하길 원하는 데이터를 프로퍼티로 추가하고, Key를 추가해서 지점 편집을 할 수 있다.
- 포인트를 찍어서 할 수도 있지만, Curves 창으로 가서 그래프를 조정하여 애니메이션을 조정할 수 있다.
    - 이렇게 조정하면 보간을 매우 쉽게 줄 수 있다.
- 직접 좌표값을 설정하는 것이 아니라, 원하는 이동경로를 줄 수 도 있다.
    - 녹화 버튼을 누른 후 원하는 시간대로 가서 오브젝트를 움직이거나 변화시키면 정말 그대로 움직이게 된다.
    - 굳이 우클릭해서 하나하나 지정하는 것 보다 이게 훨씬 간단하고 빠르고 쉬울 수 있다.

## Animation Event

- 키프레임 애니메이션에서 우클릭할 때 Add Key뿐만 아니라, Animation Event를 직접 추가할 수도 있다.
- 이는 어떤 기능을 애니메이션 시간과 함께 맞춰 사용할 수 있게 해주는 메서드 그릇이다.
- 즉, 유니티 이벤트와 완전히 유사한 기능이다.
- 만약 코드상에서 이펙트와 사운드를 출력하는 기능이 있다고 하면 그걸 시간에 맞춰 하드코딩하는 것이 아닌, 출력 메서드를 만들고 Animation Event에 맞춰 메서드를 뿌리게 하면 될 것이다.
- 단 애니메이션 이벤트를 등록만 해두고 아무런 메서드도 넣어놓지 않으면 크래시가 난다.
    - 오브젝트를 선택한 상태에서 이벤트를 등록하는 것과, 오브젝트를 선택하지 않은 상태에서 이벤트를 등록할 때 인스펙터에 뜨는 인터페이스가 다르다.
    - 아무것도 선택하지 않고 함수를 하려고 하면 이름을 직접 입력해야 하고, 오브젝트를 선택하고 고르면 해당 오브젝트에서 실행할 수 있는 메서드를 선택할 수 있는 창이 뜬다.
- 이 키프레임 애니메이션과 애니메이션 이벤트는 기존의 애니메이션 클립에도 이용할 수 있다.
    - 애니메이션 클립 칸으로 가서 인스펙터의 Events를 보면 Add Event 버튼을 볼 수 있다.
    - 원하는 시간대에 이벤트를 추가하고, 이름을 입력한다. 이후 해당 이름에 맞게 함수를 작성하면 된다.
- 또한 이벤트를 만들 때 아래에 인자들이 존재한다. Float, Int, String, Object 등이 있는데, 필요에 따라 함수에 인자를 넘기고싶을 수 있다.
    - 참고로 이 때는 같은 이름으로 오버로딩이 조금 어렵다. 먼저 이벤트를 먹은 메서드가 우선 발동된다.

# 8. UI (유저 인터페이스)

## UI 기초

- 아무거나 UI 오브젝트를 하나 만들면 Canvas와 EventSystem이 자동으로 만들어진다.
    - Canvas는 UI가 그려질 도화지같은 개념이다. 캔버스의 자식 오브젝트로만이 UI가 존재할 수 있다.
    - EventSystem은 UI에 플레이어의 클릭 등을 감지할 기본 오브젝트이다.
- UI는 Rect Transform이라는 고유의 트랜스폼 컴포넌트를 사용한다.
    - Rect Transform의 경우엔 Width나 Height 등의 다른 옵션이나, 피벗 앵커가 존재한다.
    - 피벗은 UI의 중심점을 정하는 좌표이다.
    - 앵커는 반응형 UI를 만들기 위해, 화면의 일정 구역에 위치하기 위한 화면 전체에서의 기준점이다.

## Rect Transform

- 모든 UI들이 크기와 비율이 고정이라면, 핸드폰 크기가 변할 때 UI들의 이미지들은 어떻게 변해야 할까?
    - 그냥 찌그러지게 되면 이미지가 보기 좋지 않아진다.
    - 따라서 화면비에 따라 UI가 크기가 줄어들고, 여백을 두는 것이 좋다.
- 이렇게 화면비에 따른 UI 세팅을 만들기 위해선 앵커를 잘 써야 한다.
    - 앵커는 Rect Transform을 가진 부모를 두어야 효과가 나타난다.
- 앵커 핀셋은 4개가 있고, UI의 각 꼭짓점과 매칭된다.
    - 핀셋과 부모의 크기가 비율상으로 계산되고, 핀셋과 본인의 거리는 고정된다.
    - 화면의 사이즈가 변한다면 비율이 맞게끔 움직이고, 고정된 거리만큼 옮겨진다.
- 앵커의 경우 프리셋을 활용할 수 있다.
    - Alt나 Shift를 누르며 프리셋을 눌러 피벗을 조정하고, 오브젝트의 위치를 조정할 수 있다.

## Button Event

- 캔버스의 렌더 모드를 바꾸고, 렌더 카메라를 원하는 것으로 지정도 할 수 있다.
- 버튼을 눌렀을 때의 이벤트는 On Click()을 추가해서 원하는 객체의 펑션을 이벤트에 담아주면 된다.
- 원하는 C# 스크립트에 public으로 버튼이 클릭되었을 때의 메서드를 만든다.

```csharp
public void OnClickedButton()
{
    // ...
}
```
- 이후 버튼의 이벤트에 할당해주면 되고, 옵저버 패턴과 굉장히 유사한 이벤트 방식이다.
- 만약 UI를 눌렀음에도 불구하고 게임 내 클릭 이벤트가 발생한다면, 이를 수정해주어야 한다.
    - 만약 EventSystem에서 current의 IsPointerOverGameObject() 메서드를 통해 지금 클릭된 것이 UI인지 아닌지를 반환받을 수 있다.
- 버튼을 포함한 캔버스 자체를 프리팹으로 둔 뒤, 리소스 폴더에서 해당 프리팹을 꺼내와 배치할 수 있다.
- 실전 프로젝트로 가고 게임 규모가 커질수록, 모든 함수에 다 SelializedField를 전부 할당하고, 모든 버튼 오브젝트의 인스펙터의 On Click()에 할당해주어야 한다. 이건 규모가 크면 좋지 않다.
    - 따라서 버튼에 할당하는 것 역시 코드로 하는 것이 경우에 따라 더 우아할 수 있다.
    - 이렇게 원하는 버튼에 원하는 오브젝트를 전부 게임 시작 시 연결해주는 것을 UI자동화라고 한다.

## UI 자동화

- 지금은 On Click을 등록할 때 일일히 인스펙터에서 드래그를 해 할당하고, 텍스트 컴포넌트에서 직접 컴포넌트를 가져와 무식하게 텍스트를 수정했다.
- 우선 Enum을 선언해서 모든 버튼과 텍스트에  대한 정의를 만든다.
- 이후 Bind()라는 메서드를 만들어 이름이 겹치면 연결되게끔 자동화한다.
    - 해당 Enum을 가져올 땐 직접 할당이 아닌 리플렉션을 활용한다.
- 우선 Util이라는 이름의 기본 C# 스크립트를 만들어, 필요한 기능성 메서드를 전역적으로 몰아넣는 전역 메서드 라이브러리를 하나 만들면 편리하다.

```csharp
public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
{
    if (go == null)
        return null;

    if (recursive == false)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            Transform child = go.transform.GetChild(i);
            if (string.IsNullOrEmpty(name) || child.name == name)
            {
                T component = child.GetComponent<T>();
                if (component != null)
                    return component;
            }
        }
    }
    else
    {
        foreach (T component in go.GetComponentsInChildren<T>())
        {
            if (string.IsNullOrEmpty(name) || component.name == name)
                return component;
        }
    }
}
```

```csharp
Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
enum Buttons
{
    PointButton,
}
enum Texts
{
    PointText,
    ScoreText,
}

private void Start()
{
    Bind<Button>(typeof(Buttons));
    Bind<Text>(typeof(Texts));
}

void Bind<T>(Type type) where T : UnityEngine.Object
{
    string[] names = Enum.GetNames(type);

    UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
    _objects.Add(typeof(T), objects);

    for (int i = 0; i < names.Length; i++)
    {
        objects[i] = Util.FindChild<T>(gameObject, names[i], true);
    }
}
```

- 원하는 이름의 버튼이나 텍스트를 전부 가져와 바인딩하는 것까진 자동화로 이루어진다.
- Add 이후에 for문으로 자식 오브젝트를 찾는 이유는, 딕셔너리에 Add할 때 값 자체를 넣는게 아닌 해당 배열의 주소를 기억하는 것이라, 이후에 값을 채워도 문제가 없다.

```csharp
T Get<T> (int idx) where T : UnityEngine.Object
{
    if (_objects.TryGetValue(typeof(T), out UnityEngine.Object[] objects) == false)
        return null;
    if (idx < 0 || idx >= objects.Length)
        return null;
    return objects[idx] as T;
}
```

- 이후 위와 같이 _objects에서 원하는타입의 오브젝트를 가져오는 메서드를 작성한다.
- 이 경우 원하는 오브젝트 번호를 입력만 해도 가져올 수 있게 된다.