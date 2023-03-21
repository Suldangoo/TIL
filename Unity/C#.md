<!-- TOC -->

- [C#](#c)
    - [C#에서의 변수 선언](#c%EC%97%90%EC%84%9C%EC%9D%98-%EB%B3%80%EC%88%98-%EC%84%A0%EC%96%B8)
    - [함수C#에서는 **메서드** 선언 방법](#%ED%95%A8%EC%88%98c%EC%97%90%EC%84%9C%EB%8A%94-%EB%A9%94%EC%84%9C%EB%93%9C-%EC%84%A0%EC%96%B8-%EB%B0%A9%EB%B2%95)
    - [유니티에서 기본적인 스크립트](#%EC%9C%A0%EB%8B%88%ED%8B%B0%EC%97%90%EC%84%9C-%EA%B8%B0%EB%B3%B8%EC%A0%81%EC%9D%B8-%EC%8A%A4%ED%81%AC%EB%A6%BD%ED%8A%B8)
    - [두 점 사이의 거리를 구하는 코드](#%EB%91%90-%EC%A0%90-%EC%82%AC%EC%9D%B4%EC%9D%98-%EA%B1%B0%EB%A6%AC%EB%A5%BC-%EA%B5%AC%ED%95%98%EB%8A%94-%EC%BD%94%EB%93%9C)
    - [조건문](#%EC%A1%B0%EA%B1%B4%EB%AC%B8)
    - [반복문](#%EB%B0%98%EB%B3%B5%EB%AC%B8)
    - [배열](#%EB%B0%B0%EC%97%B4)
    - [클래스와 오브젝트](#%ED%81%B4%EB%9E%98%EC%8A%A4%EC%99%80-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8)

<!-- /TOC -->

# C#

## C#에서의 변수 선언

```csharp
int gold = 1000;
gold -= 200;
```

- C#의 변수 선언은 C와 동일하다

```csharp
float itemWeight = 1.34f; // 부동소수점 형태의 경우 뒤에 f를 붙임
bool isStoreOpen = true;
char bloodType = 'A'; // char의 경우 반드시 홑따옴표를 사용
string itemName = "포션"; // string의 경우 반드시 쌍따옴표를 사용
```

- float의 경우 32비트를 사용하여 소수점 아래 7자리까지만 정확하게 표현 가능

## 함수(C#에서는 **메서드**) 선언 방법

```csharp
int Move(int hp, int distance) {
	체력 hp만큼 감소;
	오브젝트를 distance미터 옮기기;
	효과음 재생;

	return number;
}

Move(10, 30);
```

## 유니티에서 기본적인 스크립트

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptName : MonoBehaviour {

	void Start() {
		Debug.Log("Hello World!"); // 이 코드는 별도로 추가한 코드
	}

	void Update() {

	}
}
```

- **ScriptName과 해당 cs파일의 이름은 반드시 같아야함**!! 안그러면 패키지 오류
- using 키워드로 라이브러리를 임포트할 수 있음
    - using 뒤에 오는 경로를 네임스페이스(namespace)라고 함
- start() 메서드는 게임이 시작될 때 자동으로 한 번 실행됨
- 주석은 //로 한 줄, /* ~ */ 로 여러줄을 씌울 수 있음
- Debug.Log()는 사실 UnityEngine 경로 안에 있는 명령어

```csharp
Debug.Log("키 : " + height); // +로 여러개를 한 번에 출력 가능
```

## 두 점 사이의 거리를 구하는 코드

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptName : MonoBehaviour {

	void Start() {
		float distance = GetDistance(2, 2, 5, 6);
		Debug.Log("(2, 2)에서 (5, 6)까지의 거리 : " + distance);
	}

	float GetDistance(float x1, float x2, float y1, float y2) {
		float width = x2 - x1;
		float height = y2 - y1;

		float distance = width * width + height * height;
		distance = Mathf.Sqrt(distance);

		return distance;
	}
}
```

- distance가 두 개 사용되었지만 각각 다른 메서드에서 사용되었음. 즉, 변수는 메서드 내부에서만 유효하며, 이러한 유효 범위를 **스코프**라고 함

## 조건문

```csharp
if (love > 70) {
	Debug.Log("굿 엔딩");
}
else if (love > 50) {

}
else {

}
```

- 연산자는 다음과 같다.
    - <, <=, >, >=, ==, !=
    - AND : &&
    - OR : ||
    - NOT : !
    

## 반복문

```csharp
for (int i = 0; i < 10; i++) {

}

while(i < 10) {
	...
	i++;
}
```

## 배열

```csharp
int[] students = new int[5];

students[0] = 100;
```

- int[]를 이용하여 정수형 배열임을 선언
- 뒤에 new int[5]; 를 넣어 int형의 5칸짜리 배열을 새로 만들어 할당

## 클래스와 오브젝트

- 클래스 : 표현하고 싶은 대상을 **추상화**하여, 대상과 관련된 변수와 메서드를 정의하는 틀
- 오브젝트 : 실제로 존재하는 물건(실체). 클래스를 기반으로 만들어지는 독립적인 개체
    - 클래스라는 틀로 오브젝트를 찍어내는 것을 **인스턴스화** 라고 함
    - 클래스로 인스턴스화시킨 오브젝트를 **인스턴스**라고 함. 즉 오브젝트 = 인스턴스

- int name = 4000; 같은 것은 new를 사용하지 않음
- 그러나 Animal tom = new Animal(); 은 new를 사용함
- new를 사용하는 것은 오브젝트가 없는 것이므로, 클래스를 사용해 그 자리에서 인스턴스화시키는 것. 그리고 new를 사용하면 참조타입의 변수가 됨
    - 참조타입의 변수면, 참조타입으로 같은 클래스를 참조한 변수에 다른 변수를 적용시키면 덮어씌워지는 것이 아닌 그 변수의 주소가 바뀌게 됨
    - 그래서 변수1 = 변수2 한다음 변수1 내용 바꾸면 변수2 내용도 바뀜 (주소라서)