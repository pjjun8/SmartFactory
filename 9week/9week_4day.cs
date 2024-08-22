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
