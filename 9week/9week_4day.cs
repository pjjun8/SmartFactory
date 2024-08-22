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
