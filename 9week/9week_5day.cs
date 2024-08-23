//오늘은 늦잠을 자서 오후부터 수업에 참여했다. 오전에는 HTML5CSS3 교제 7~8장을 했다고한다.
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <style>
        .items{
            border: solid 3px #000;
            margin: 5px;
            float: left;
        }
        .items img{
            display: block;
        }
        .clear{
            clear: left;
        }

    </style>
</head>
<body>
    <div class="items"><img src="./img/image1.jpg"></div>
    <div class="items"><img src="./img/image2.jpg"></div>
    <div class="items"><img src="./img/image3.jpg"></div>
    <div class="clear"></div>
    <div class="items"><img src="./img/image4.jpg"></div>
    <div class="items"><img src="./img/image5.jpg"></div>
    <div class="items"><img src="./img/image6.jpg"></div>

</body>
</html>
=======================================================================
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <style>
        *{
            margin: 0;
            padding: 0;
        }
        body{
            background-color: #f2f0f0;
            font-family: "맑은 고딕";
            font-size: 12px;
            color: #444444;
        }
        ul{
            list-style-type: none;
        }
        .clear{
            clear: both;
        }
        #logo{
            padding: 30px 0 0 30px;
            float: left;
        }
        #top_menu{
            margin: 48px 10px 0 0;
            float: right;
        }
        #top_menu li{
            display: inline;
        }
        #main_menu{
            font-size: 12px;
            color: #ffffff;
            background-color: #4e4c4d;
            margin-top: 15px;
            padding: 15px;
            text-align: center;
        }
        #main_menu li{
            padding: 0 20px 0 20px;
            display: inline;
        }
    </style>
</head>
<body>
    <div id="logo">
        <img src="img/logo.gif">
    </div>
    <ul id="top_menu">
        <li>로그인 :</li>
        <li>회원가입 :</li>
        <li>마이페이지 :</li>
        <li>주문배송 조회 :</li>
        <li>장바구니 :</li>
        <li>이용안내 :</li>
        <li>고객센터</li>
    </ul>
    <div class="clear"></div>

    <ul id="main_menu">
        <li>다은아트 소개</li>
        <li>상품 Q&A</li>
        <li>시안 확인</li>
        <li>고객 갤러리</li>
        <li>공지사항</li>
    </ul>
</body>
</html>
================================================================
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <style>
        
        .clear{
            clear: both;
        }
        header{
            width: 800px;
            height: 60px;
            margin: 2px;
            border:solid 2px #ff0000;
        }
        aside{
            width: 175px;
            height: 398px;
            float: left;
            padding: 2px;
            border: solid 2px #ff0000;
        }
        nav{
            height: 150px;
            margin-bottom: 50px;
            margin: 2px;
            border:solid 2px #00f;
        }
        
        main{
            border: solid 2px #00f;
            float: left;
            width: 618px;
            float: left;
            height: 400px;
            margin: 2px;
        }
        section{
            width: 500px;
            height: 150px;
            margin:  2px;
            border: solid 2px #0f0;
        }
        footer{
            width: 800px;
            height: 90px;
            margin: 2px;
            border: solid 2px #ff0000;
        }
    </style>
</head>
<body>
    <header>상단 헤더</header>
    <aside>좌측
        <nav>메뉴</nav>

    </aside>
    <main> 메인 콘텐츠
        <section>콘텐츠1</section>
        <section>콘텐츠2</section>

    </main>
    <div class="clear"></div>
    <footer>하단풋터</footer>
</body>
</html>
