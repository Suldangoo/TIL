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

### AActor

- UObject를 상속받은, 게임 월드 내에서 배치할 수 있는 오브젝트.
- 트랜스폼이 있으며, Tick()을 사용하여 매 프레임 업데이트할 수 있다.
- UComponent들을 포함할 수 있다.
    - 이는 UActorComponent를 상속받아 AActor에 부착할 수 있는 부품형 객체이다.
    - 독립적으로 동작 가능하지만 반드시 액터에 부착해야 한다.

### 객체 라이프사이클

- Unreal에서의 모든 객체는 라이프사이클을 지니고 있다.
    - 객체가 생성되고, 동작하고, 제거되는 과정을 말한다.
    - AActor 기준으로 서술한다.
- 객체 생성
    - 생성자는 AActor::AActor()에서 호출되며, UObject는 절대로 new 연산자를 사용해선 안된다. 모든 UObject는 언리얼 엔진으로 관리되는 메모리이며, 가비지 컬렉션된다. new 또는 delete를 사용하여 메모리를 수동으로 관리하면 메모리가 손상될 수 있다.
    - BeginPlay()은 월드에 액터가 스폰된 직후 한 번만 호출된다.
- 게임 인스턴스
    - Init() : 게임 인스턴스가 생성될 때 호출된다. 초기화 작업에 사용된다.
    - OnStart() : 첫 번째 월드가 로드된 후 호출된다. 게임 시작 전 필요한 추가 설정 수행에 사용한다.
    - Shutdown() : 게임이 종료될 때 호출된다. 전체 리소스 정리에 사용된다.
- 실행 중
    - Tick(float DeltaTime)은 매 프레임마다 호출된다.
    - 이는 bCanEverTick = true인 경우에만 실행되며, SetActorTickEnabled(false);로 비활성화 가능하다.
- 제거
    - EndPlay()는 액터가 삭제될 때 호출된다.
    - Destroyed()는 액터가 완전히 삭제될 때 호출된다.

### 가비지 컬렉션 (Garbage Collection)

- 언리얼 엔진은 UObject 기반의 객체를 자동으로 관리하는 GC 시스템을 사용한다.
- UPROPERTY() 매크로를 통해 UObject 기반 객체를 GC 대상에 포함시킬 수 있고, 하지 않으면 GC가 객체를 인식하지 못하고 삭제될 수 있다.
    - 이를 통해 UE가 새 클래스, 함수, 변수를 인식하게 한다.

### 스마트 포인터

- 언리얼은 가비지 컬렉터 뿐만이아니라 스마트 포인터로도 메모리를 관리한다. 두 개념 모두 C++로 자체 제작한 코드를 이용하며, 순수 C++ 이용 상황에선 스마트 포인터를 사용하고, UObject와 관련된 부분에서는 GC를 사용한다.
    - 즉 new를 사용한 객체는 GC가 관리할 수 없어서, 스마트 포인터를 쓴다.
- TSharedPtr이 일반적인 C++ 스마트 포인터이며, UObject의 안전한 약한 포인터는 TWeakObjectPtr로 사용하기도 한다.

---

## **2. C++ 및 블루프린트 연동**

<aside>
🎮

- **블루프린트와 C++의 관계**: `UCLASS()`, `UFUNCTION()`, `UPROPERTY()` 매크로의 역할
- **블루프린트 함수 라이브러리**: C++에서 블루프린트 전용 함수 제공하기
- **인터페이스와 이벤트 시스템**: `BlueprintImplementableEvent`, `BlueprintNativeEvent`
- **Gameplay Framework 주요 클래스**: `ACharacter`, `AController`, `AGameModeBase`, `UUserWidget` 등
</aside>

- Unreal Engine의 모든 기능은 C++과 블루프린트를 함께 사용해 구현한다.
    - 게임 개발자가 C++로 성능이 중요한 핵심 기능을 만들고, 블루프린트로 게임 디자이너들이 쉽게 조작할 수 있도록 구현하는 편이다.

### 블루프린트와 C++

- 언리얼에서 C++ 클래스를 만들면 블루프린트로 확장해서 사용할 수 있다.
- UCLASS()를 통해 언리얼이 이 클래스를 인식하고, 블루프린트에서 상속할 수 있게 한다.
- 변수에 UPROPERTY()를 붙여 블루프린트에서 볼 수 있고, 수정 가능하게 한다.
    - 결과적으로 C++ 변수를 블루프린트에서 조작 가능하게 한다.
- UFUNCTION()을 통해 블루프린트에서 함수 호출이 가능하다.

| 부모 클래스 | 역할 | 언제 사용? | 예제 |
| --- | --- | --- | --- |
| **AActor** | 기본적인 게임 오브젝트 | 게임 내 오브젝트(아이템, 문, 상호작용 오브젝트 등) | `AWeapon`, `AChest` |
| **ACharacter** | 이동, 점프, 애니메이션 포함된 캐릭터 | 플레이어, NPC, 적 캐릭터를 만들 때 | `APlayerCharacter`, `AEnemyCharacter` |
| **APawn** | 직접 제어 가능한 오브젝트 (Character보다 기능 적음) | 캐릭터 이외의 조종 가능한 것 (예: 탱크, 비행기) | `AVehiclePawn`, `AJetPawn` |
| **APlayerController** | 플레이어의 입력을 처리 | 키보드/마우스 입력, UI 클릭 처리 | `AMyPlayerController` |
| **AAIController** | AI가 캐릭터를 조종하도록 함 | NPC나 몬스터의 AI 동작 | `AEnemyAIController` |
| **AGameModeBase** | 게임의 규칙을 정함 | 게임 시작 조건, 승리 조건, 스폰 관리 | `AMyGameMode` |
| **AGameStateBase** | 현재 게임의 상태 저장 | 점수, 진행 상황 관리 | `AMyGameState` |
| **APlayerState** | 플레이어별 정보 저장 | 플레이어 이름, 점수, 핑 | `AMyPlayerState` |
| **UUserWidget** | UI 요소 | 화면에 표시할 HUD, 메뉴 | `UMyHUDWidget` |

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

- 언리얼 엔진에서의 협업은 Perforce를 쓴다. 이는 줄여서 P4라고도 부른다.
- 이는 대용량 파일 (에셋, 맵, 애니메이션 등) 을 효과적으로 관리할 수 있다.

### Perforce

- Perforce(정식 명칭: **Helix Core**)는 **중앙 집중형 버전 관리 시스템(CVCS)이다.**
- Git처럼 분산형(DVCS)이 아니라 **모든 데이터가 중앙 서버(P4 서버)에 저장된다.**
- 팀원들이 같은 파일을 동시에 수정하는 경우에 충돌을 막고 관리하기 편하다.
    - Locking이 가능하기 때문에 충돌을 미리 방지할 수 있다.
- 서버 기반이라 매우 빠르다.

| 명령어 | Git에서의 유사 명령어 | 기능 |
| --- | --- | --- |
| p4 sync | git pull | 서버에서 최신 파일을 로컬로 가져온다. |
| p4 sync //... |  | //…는 모든 파일을 동기화한다는 뜻이다. |
| p4 edit 파일명 | git은 그냥 수정하면 되지만, 퍼포스는 명시적으로 체크아웃해야 한다. | 수정하려는 파일을 서버에 ‘내가 수정중’이라고 알리는 것이다.
다른 사람이 파일을 수정하지 못하도록 Lock도 걸 수 있다. |
| p4 submit | git commit + git push | 변경한 파일을 서버에 업로드 (커밋) 하는 것이다. |
| p4 add | git add | 새로운 파일을 버전 관리에 추가. |
| p4 delete | git rm | 파일을 삭제하는 명령어이다. |
| p4 submit |  | 서버에 반영 및 동기화하는 명령어이다. add나 delete 뒤에 붙는다. |
| p4 revert | git checkout — 파일명 | 변경한 내용을 되돌림. 즉, 서버에서 받은 원본 상태로 복구 |
| p4 changes | git log | 최근 변경된 사항들을 확인 |
| p4 opened |  | -a 옵션을 붙이면 현재 팀원들이 어떤 파일을 체크아웃했는지 확인 |

- Perforce의 기본적인 워크플로우
    1. p4 sync //… 를 통해 최신 파일 가져오기
    2. p4 edit 파일명을 통해 수정할 파일 체크아웃
        - p4 lock 파일명으로 잠금 설정 가능
    3. 파일 수정
    4. p4 submit -d “설명” 을 통해 변경사항 저장