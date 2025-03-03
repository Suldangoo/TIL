## **1. Unreal Engine 5의 기본 구조 및 라이프사이클**

<aside>
🎮

- **언리얼 객체 모델**: `UObject`, `AActor`, `UComponent` 등 계층 구조 이해
- **라이프사이클**: `BeginPlay()`, `Tick()`, `EndPlay()` 등의 호출 순서
- **가비지 컬렉션**: `UProperty`, `UPROPERTY()` 매크로 및 스마트 포인터 (`TSharedPtr`, `TWeakPtr`, `TUniquePtr`)
</aside>

### UObject (언리얼 오브젝트)

- 기본적으로 C++로 프로그래밍할 때, 게임플레이 클래스를 생성해서 VS 등으로 컴파일 후 언리얼 에디터에 반영하여 사용한다. 당연히 표준 C++ 구문을 사용한다.
- 언리얼 엔진 오브젝트의 베이스 클래스가 UObject.
- UClass 매크로는 UObject에서 파생된 클래스에 태그를 지정하여 UObject 처리 시스템이 인식하도록 할 수 있다.
- 가비지컬렉션의 대상이 되는 객체이며, 반사(reflection) 시스템을 지원한다.
- 그러나 트랜스폼(위치, 회전, 스케일)이 없다. 3D 공간에 배치되려면 AActor를 상속받아야 한다.



---

## **2. C++ 및 블루프린트 연동**

<aside>
🎮

- **블루프린트와 C++의 관계**: `UCLASS()`, `UFUNCTION()`, `UPROPERTY()` 매크로의 역할
- **블루프린트 함수 라이브러리**: C++에서 블루프린트 전용 함수 제공하기
- **인터페이스와 이벤트 시스템**: `BlueprintImplementableEvent`, `BlueprintNativeEvent`
- **Gameplay Framework 주요 클래스**: `ACharacter`, `AController`, `AGameModeBase`, `UUserWidget` 등
</aside>

---

## **3. 게임 시스템 및 메커니즘 구현**

<aside>
🎮

- **컴포넌트 기반 시스템**: `UActorComponent`, `USceneComponent` 활용
- **데이터 관리 및 저장**: `SaveGame` 시스템 활용
- **애니메이션 시스템**: 애니메이션 블루프린트, `UAnimInstance` 사용법
- **입력 시스템**: `Enhanced Input` 플러그인 활용
- **멀티스레딩 및 성능 최적화**: `AsyncTask`, `FRunnable`, `TaskGraph` 등
</aside>

---

## **4. AI, 물리, 렌더링, 네트워킹**

<aside>
🎮

- **AI**: `Behavior Tree`, `Blackboard`, `NavMesh` 활용법
- **물리**: `Chaos Physics` 기본 개념, `UPhysicsConstraintComponent` 활용
- **렌더링**: LOD, Virtual Shadow Map, Nanite, Lumen 개념
- **네트워킹**: `Replication`, `RPC`, `Server-Client` 구조
</aside>

---

## **5. 게임 성능 분석 및 최적화**

<aside>
🎮

- **Unreal Insights 활용법**
- **프로파일링 및 최적화**: `STAT`, `r.ProfileGPU`, `MemReport` 등의 명령어
- **렌더링 최적화**: `Instanced Static Mesh`, `Occlusion Culling`
- **CPU 최적화**: `Object Pooling`, `Async Loading`
</aside>

---

## **6. 프로젝트 관리 및 협업 도구**

<aside>
🎮

- **Perforce 개념 및 워크플로우**: Git과의 차이점 이해, `p4 sync`, `p4 submit`
- **코드 리뷰 및 협업**: Clean Code 원칙, `Coding Standard` 확인
- **노션 및 슬랙 활용법**: 팀 내 문서화 및 커뮤니케이션 최적화
</aside>