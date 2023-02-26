# **목차**
- [**목차**](#목차)
- [**Markdown (마크다운)**](#markdown-마크다운)
- [**마크다운 문법**](#마크다운-문법)
  - [1. 제목 (Headers)](#1-제목-headers)
  - [2. 목록](#2-목록)
  - [3. 코드 삽입](#3-코드-삽입)
  - [4. 인용 문구 (BlockQuote)](#4-인용-문구-blockquote)
  - [5. 수평선](#5-수평선)
  - [6. 강조](#6-강조)
  - [7. 줄 바꿈](#7-줄-바꿈)
  - [8. 링크](#8-링크)
  - [9. 테이블](#9-테이블)
  - [10. 기타](#10-기타)

# **Markdown (마크다운)**
>마크다운(Markdown)은 2004년 존 그루버가 제작한 일반 텍스트 기반의 마크업 언어이다.
- 일반 텍스트로 서식이 있는 문서를 작성할 때 사용한다.
- HTML로 변환이 가능하며, 깃허브 내 리포지토리의 README.md 또한 마크다운 문서이다.
- 확장자명은 .md이다.
- 깃허브 내에서 .md파일을 편집할 수도 있으나, 별도의 편집기를 활용한 후 push를 하는 방법도 있다.
  
<br>
<p></p>

# **마크다운 문법**

## 1. 제목 (Headers)
- #을 사용하는 방법

```
# 제목을 입력
###### 최대 6개까지 가능하며, 많을 수록 폰트의 크기가 작아진다
```

- '==='를 사용하는 방법

```
===
제목
---
부제목
```

<br>

## 2. 목록
- 순서가 없는 목록

```
- 순서가 없는 목록은
* '-' 혹은 '+', '*'를 앞에 입력해 작성 가능하다
```

- 순서가 있는 목록

```
1. 순서가 있는 목록은
2. '1.' 처럼 숫자와 점을 입력해 작성 가능하다
```

- 혼합 사용

```
* 1단계
    - 2단계
        + 3단계
```

* 1단계
    - 2단계
        + 3단계

<br>

## 3. 코드 삽입
- 4개의 공백 혹은 하나의 탭으로 코드 삽입

```
    코드...
````

- '```'를 사용해 코드 삽입

<pre>
```
코드...
```
</pre>
>git에서는 첫 번째 '```' 뒤에 사용한 언어를 작성하여 문법을 강조할 수도 있다.

- 'pre' 태그를 사용해 코드 삽입

```
<pre>
<code>
코드...
</code>
</pre>
```

<br>

## 4. 인용 문구 (BlockQuote)

```
> 인용 문구
>   > 서브 인용문구
```
> 인용 문구
>   > 인용문구

<br>

## 5. 수평선

```
***
---
<hr>
```
***
---
<hr>

<br>

## 6. 강조

```
*이태릭체*
**볼드체**
~~취소선~~
```

*이태릭체*   
**볼드체**   
~~취소선~~

<br>

## 7. 줄 바꿈

```
문장 마지막에서 띄어쓰기를 세 칸 넣거나,   
다른 방법으로 <br> 태그를 사용하거나,
마지막 방법으로 <p></p> 를 사용하면 줄이 바뀐다.
```

문장 마지막에서 띄어쓰기를 세 칸 넣거나,   
다른 방법으로 <br> 태그를 사용하거나,
마지막 방법으로 <p> </p> 를 사용하면 줄이 바뀐다.

<br>

## 8. 링크

- 참조 링크

```
참조 링크는 대괄호 두 개를 사용한다.

Link : [Google][googlelink]

[googlelink] : https://google.com "Go google"
```

Link: [Google][googlelink]

[googlelink]: https://google.com "Go google"

- 외부 링크

```
외부 링크는 대괄호 하나와 소괄호 하나를 사용한다.

[Google](https://google.com)
```

[Google](https://google.com)

```
이를 이용하면 리포지토리 내에서 md 문서간의 이동 혹은 헤더로의 이동을 할 때도 사용할 수 있다.
편집기를 활용하면 자동완성 덕분에 쉽게 입력이 가능하다.

[예시 1. README.md로 이동](../README.md)   
[예시 2. Markdown.md의 제목 문단으로 이동](#1-제목-headers)
```

[예시 1. README.md로 이동](../README.md)   
[예시 2. Markdown.md의 제목 문단으로 이동](#1-제목-headers)

- 자동 연결

```
자동 연결은 홑화살괄호 하나를 사용한다.
일반적인 URL 혹은 이메일 주소는 해당 방식이 적절하다.

- 외부 링크 : <https://google.com>
- 이메일 링크 : <example@example.com>
```

- 외부 링크 : <https://google.com>
- 이메일 링크 : <example@example.com>

<br>

## 9. 테이블

- Markdown 코드를 활용한 테이블

```
<!-- Markdown -->
Title1|Title2
-|-
content1|content2
content3|content4
  
Title1|Title2|Title3
:-|:-:|-:
content1|content2|content3
```

<!-- Markdown -->
Title1|Title2
-|-
content1|content2
content3|content4
  
Title1|Title2|Title3
:-|:-:|-:
content1|content2|content3

- Html 코드를 활용한 테이블

```
<!-- Html -->
<figure>
    <table>
        <thead>
            <tr>
                <th>Title1</th>
                <th>Title2</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>content1</td>
                <td>content2</td>
            </tr>
            <tr>
                <td>content3</td>
                <td>content4</td>
            </tr>
        </tbody>
    </table>
</figure>
  
<figure>
    <table>
        <thead>
            <tr>
                <th style='text-align:left;' >Title1</th>
                <th style='text-align:center;' >Title2</th>
                <th style='text-align:right;' >Title3</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style='text-align:left;' >content1</td>
                <td style='text-align:center;' >content2</td>
                <td style='text-align:right;' >content3</td>
            </tr>
        </tbody>
    </table>
</figure>
```

<!-- Html -->
<figure>
    <table>
        <thead>
            <tr>
                <th>Title1</th>
                <th>Title2</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>content1</td>
                <td>content2</td>
            </tr>
            <tr>
                <td>content3</td>
                <td>content4</td>
            </tr>
        </tbody>
    </table>
</figure>
  
<figure>
    <table>
        <thead>
            <tr>
                <th style='text-align:left;' >Title1</th>
                <th style='text-align:center;' >Title2</th>
                <th style='text-align:right;' >Title3</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style='text-align:left;' >content1</td>
                <td style='text-align:center;' >content2</td>
                <td style='text-align:right;' >content3</td>
            </tr>
        </tbody>
    </table>
</figure>

<br>

## 10. 기타

- [MD 목차(TOC)를 쉽게 만들어주는 사이트](https://ecotrust-canada.github.io/markdown-toc/)