# **����**
- [**����**](#����)
- [**Markdown (��ũ�ٿ�)**](#markdown-��ũ�ٿ�)
- [**��ũ�ٿ� ����**](#��ũ�ٿ�-����)
  - [1. ���� (Headers)](#1-����-headers)
  - [2. ���](#2-���)
  - [3. �ڵ� ����](#3-�ڵ�-����)
  - [4. �ο� ���� (BlockQuote)](#4-�ο�-����-blockquote)
  - [5. ����](#5-����)
  - [6. ����](#6-����)
  - [7. �� �ٲ�](#7-��-�ٲ�)
  - [8. ��ũ](#8-��ũ)
  - [9. ���̺�](#9-���̺�)
  - [10. ��Ÿ](#10-��Ÿ)

# **Markdown (��ũ�ٿ�)**
>��ũ�ٿ�(Markdown)�� 2004�� �� �׷���� ������ �Ϲ� �ؽ�Ʈ ����� ��ũ�� ����̴�.
- �Ϲ� �ؽ�Ʈ�� ������ �ִ� ������ �ۼ��� �� ����Ѵ�.
- HTML�� ��ȯ�� �����ϸ�, ����� �� �������丮�� README.md ���� ��ũ�ٿ� �����̴�.
- Ȯ���ڸ��� .md�̴�.
- ����� ������ .md������ ������ ���� ������, ������ �����⸦ Ȱ���� �� push�� �ϴ� ����� �ִ�.
  
<br>
<p></p>

# **��ũ�ٿ� ����**

## 1. ���� (Headers)
- #�� ����ϴ� ���

```
# ������ �Է�
###### �ִ� 6������ �����ϸ�, ���� ���� ��Ʈ�� ũ�Ⱑ �۾�����
```

- '==='�� ����ϴ� ���

```
===
����
---
������
```

<br>

## 2. ���
- ������ ���� ���

```
- ������ ���� �����
* '-' Ȥ�� '+', '*'�� �տ� �Է��� �ۼ� �����ϴ�
```

- ������ �ִ� ���

```
1. ������ �ִ� �����
2. '1.' ó�� ���ڿ� ���� �Է��� �ۼ� �����ϴ�
```

- ȥ�� ���

```
* 1�ܰ�
    - 2�ܰ�
        + 3�ܰ�
```

* 1�ܰ�
    - 2�ܰ�
        + 3�ܰ�

<br>

## 3. �ڵ� ����
- 4���� ���� Ȥ�� �ϳ��� ������ �ڵ� ����

```
    �ڵ�...
````

- '```'�� ����� �ڵ� ����

<pre>
```
�ڵ�...
```
</pre>
>git������ ù ��° '```' �ڿ� ����� �� �ۼ��Ͽ� ������ ������ ���� �ִ�.

- 'pre' �±׸� ����� �ڵ� ����

```
<pre>
<code>
�ڵ�...
</code>
</pre>
```

<br>

## 4. �ο� ���� (BlockQuote)

```
> �ο� ����
>   > ���� �ο빮��
```
> �ο� ����
>   > �ο빮��

<br>

## 5. ����

```
***
---
<hr>
```
***
---
<hr>

<br>

## 6. ����

```
*���¸�ü*
**����ü**
~~��Ҽ�~~
```

*���¸�ü*   
**����ü**   
~~��Ҽ�~~

<br>

## 7. �� �ٲ�

```
���� ���������� ���⸦ �� ĭ �ְų�,   
�ٸ� ������� <br> �±׸� ����ϰų�,
������ ������� <p></p> �� ����ϸ� ���� �ٲ��.
```

���� ���������� ���⸦ �� ĭ �ְų�,   
�ٸ� ������� <br> �±׸� ����ϰų�,
������ ������� <p> </p> �� ����ϸ� ���� �ٲ��.

<br>

## 8. ��ũ

- ���� ��ũ

```
���� ��ũ�� ���ȣ �� ���� ����Ѵ�.

Link : [Google][googlelink]

[googlelink] : https://google.com "Go google"
```

Link: [Google][googlelink]

[googlelink]: https://google.com "Go google"

- �ܺ� ��ũ

```
�ܺ� ��ũ�� ���ȣ �ϳ��� �Ұ�ȣ �ϳ��� ����Ѵ�.

[Google](https://google.com)
```

[Google](https://google.com)

```
�̸� �̿��ϸ� �������丮 ������ md �������� �̵� Ȥ�� ������� �̵��� �� ���� ����� �� �ִ�.
�����⸦ Ȱ���ϸ� �ڵ��ϼ� ���п� ���� �Է��� �����ϴ�.

[���� 1. README.md�� �̵�](../README.md)   
[���� 2. Markdown.md�� ���� �������� �̵�](#1-����-headers)
```

[���� 1. README.md�� �̵�](../README.md)   
[���� 2. Markdown.md�� ���� �������� �̵�](#1-����-headers)

- �ڵ� ����

```
�ڵ� ������ Ȭȭ���ȣ �ϳ��� ����Ѵ�.
�Ϲ����� URL Ȥ�� �̸��� �ּҴ� �ش� ����� �����ϴ�.

- �ܺ� ��ũ : <https://google.com>
- �̸��� ��ũ : <example@example.com>
```

- �ܺ� ��ũ : <https://google.com>
- �̸��� ��ũ : <example@example.com>

<br>

## 9. ���̺�

- Markdown �ڵ带 Ȱ���� ���̺�

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

- Html �ڵ带 Ȱ���� ���̺�

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

## 10. ��Ÿ

- [MD ����(TOC)�� ���� ������ִ� ����Ʈ](https://ecotrust-canada.github.io/markdown-toc/)