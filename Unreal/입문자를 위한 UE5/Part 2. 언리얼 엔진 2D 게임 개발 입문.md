# 개요

## 환경 설정

- 템플릿은 사용하지 않고, Games - Black 프로젝트로 시작
- Ctrl + N으로 새로운 레벨 생성 - 빈 레벨 생성
    - 조명이 없어서 완전히 까만 상태
    - 그러나 우리가 3D 게임을 만들게 아니기에 상관 없음
- 편집 - 프로젝트 세팅 - 맵 & 모드
    - 에디터 시작 맵과 게임 기본 맵을 새로 만든 맵으로 설정

## Unreal vs Unity

- 유니티는 깡통 오브젝트를 하나 만들고, 컴포넌트를 이리저리 조립해서 만든다.
    - 때문에 굉장히 자유도가 높고 직관적이다.
    - Create Empty를 선택하면 Transform 컴포넌트 외엔 아무런 컴포넌트도 없는데, 여기에 어떤 **컴포넌트**를 추가하냐에 따라 정말 그 무엇이든 될 수 있다.
    - 그 컴포넌트는 유니티에서 기본 제공하는 것 외에, C# 스크립팅을 통해 직접 만들 수 있다.
    - 카메라든, 조명이든, 캐릭터든, 매니저든 이것저것 조립을 통해 만들게 되는 것이다.
- 언리얼은 Empty를 만드는 기능은 없고, 처음부터 오브젝트를 어떠한 목적으로 만들지 정해준다.
    - 똑같이 큐브를 만들어도, 이미 타입이 Static Mesh Actor로써, 액터를 상속받아 만들어진다.
    - 클래스를 만들더라도 깡통 클래스를 만들 수 없고, 무언가를 반드시 상속받아야 한다.
    - 즉, **상속**이 굉장히 중요한 개념으로 자리잡혀있다.
- 유니티는 백지부터 만들기에 만드는 사람들마다 노하우도 다르고, 방법도 다르다.
- 그러나 언리얼은 정답에 가까운 구조가 있고, 엔진 공부를 하며 게임을 만들어야 한다.
- 즉, **유니티는 컴포넌트**가 핵심이고, **언리얼은 상속**이 핵심이다.