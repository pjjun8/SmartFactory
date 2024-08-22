//태이블 작성
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <style>
        table, th, tr, td{
            border:solid 2px black;
            border-collapse: collapse;
            padding: 9px;
            margin: 30px;
        }
        
    </style>
</head>
<body>
    <table>
        <tr>
            <td>지역</td>
            <td>현재기온</td>
            <td colspan="2">불쾌지수/습도(%)</td>
            <td>풍속</td>
            
        </tr>
        <tr>
            <td rowspan="2">서울/경기</td>
            <td>23</td>
            <td>60</td>
            <td>88</td>
            <td>4.7</td>
        </tr>
        <tr>
            <td>20</td>
            <td>60</td>
            <td>80</td>
            <td>5.0</td>            
        </tr>
        <tr>
            <td rowspan="2">제주도</td>
            <td>21</td>
            <td>65</td>
            <td>85</td>
            <td>3.6</td>
        </tr>
    </table>
</body>
</html>
================================================================
//폼 양식 작성
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
</head>
<body>
    <Form>
        이름 : <INPUT TYPE="text"></br>
        나이 : <INPUT TYPE="text"><p></p>

        비밀번호 : <INPUT TYPE="password"><p></p>
        개인정보 : <INPUT TYPE="radio" NAME="info" checked> 공개
                <INPUT TYPE="radio" NAME="info"> 비공개<p></p>
        이메일 : <INPUT TYPE="text">@
            <Select>
                <OPTION>선택</OPTION>
                <OPTION>naver.com</OPTION>
                <OPTION>gmaile.com</OPTION>
                <OPTION>daum.net</OPTION>
                <OPTION>직접입력</OPTION>
            </Select><p></p>
            자기소개 : <br>
            <textarea rows="10" cols="60"></textarea><br>

            <button type="button">검색</button>
            <button type="submit">확인</button>
            <button type="reset">다시쓰기</button>


    </Form>
</body>
</html>
=============================================================
<!--MyStyle.CSS-->
h2{
            color : #ff0000;
            font-size : 18px;
 }
=============================================================
<!--CSS2.html-->
<!DOCTYPE html>
<html lang="ko">
<head>
<meta charset="UTF-8">
<title>외부 스타일 시트</title>
<link rel="stylesheet" type="text/CSS" href="./CSS/MyStyle.CSS">
</head>
<body>
    <h2>제목입니다.</h2>
</body>
</html>
=============================================================
<!--CSS 링크-->
<!DOCTYPE html>
<html lang="ko">
<head>
    <style>
        p{
            line-height: 250%;
            font-weight: bolder;
        }
        h3{
            font-family: "맑은 고딕";
            color: blue;
            text-shadow: 3px 3px 5px #555;
        }
        h4{
            font-style: italic;
        }
        a:link{
            color:#0000ff;
            text-decoration:none;
        }
        a:visited{
            color:#00ff00;
        }
        a:hover{
            color:#ff0000;
            font-weight: bold;
            text-decoration: underline;
        }
        a:active{
            color:#00ffff;
        }
    </style>
</head>
<body>
    <h2>로즈메리 허브</h2>

    <p>로즈메리는 남유럽이 원산지이며 1~2미터까지 자라는 여러해살이 풀이다. 2~6월에 연보라색, 청자색, 연분홍, 흰색 꽃이 핀다.</p>
    <h3>페퍼민트 허브</h3>
    <h4>안녕하세요!</h4>
    <a href="#">자유게시판</a>
</body>
</html>
===============================================================
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <title>Document</title>
    <style>
        p{
            color:#444444;
            line-height:150%;
        }
        #position{
            color:#ff0000;
            font-weight:bold;
        }
        .weather{
            color:#0000ff;
            font-weight: bold;
        }
        #kind{
            color:#00ff00;
            font-style:italic;
            text-decoration: underline;
        }
    </style>
<body>
    <h3>다육식물</h3>
    <p>다육 식물은 <span class="weather">내부</span>에 물을 저장하고 있기 떄문에 다른 식물에 비해  <span id="position">통통한</span> 외관을 가지는 경우가 많다. 이렇한 특성을 다육질이라고 부르는데 다육 식물은 이러한 특성 이외에도 물을 절약하기 위해 다음과 같은 다양한 특성을 가지고 있다.</p>
    <p>다육으로 된 부위에 따라 다음과 같이 <span id="kind">분류할</span> 수 있다.</p>
</body>
</html>
============================================================
//6장 CSS선택자 내용 중 ID선택자
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <title>Document</title>
    <style>
        p{
            color:#444444;
            line-height:150%;
        }
        #position{
            color:#ff0000;
            font-weight:bold;
        }
        .weather{
            color:#0000ff;
            font-weight: bold;
        }
        #kind{
            color:#00ff00;
            font-style:italic;
            text-decoration: underline;
        }
    </style>
<body>
    <h3>다육식물</h3>
    <p>다육 식물은 <span class="weather">내부</span>에 물을 저장하고 있기 떄문에 다른 식물에 비해  <span id="position">통통한</span> 외관을 가지는 경우가 많다. 이렇한 특성을 다육질이라고 부르는데 다육 식물은 이러한 특성 이외에도 물을 절약하기 위해 다음과 같은 다양한 특성을 가지고 있다.</p>
    <p>다육으로 된 부위에 따라 다음과 같이 <span id="kind">분류할</span> 수 있다.</p>
</body>
</html>
