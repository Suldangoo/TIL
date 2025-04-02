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