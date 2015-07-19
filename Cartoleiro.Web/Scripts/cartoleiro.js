(function () {
    $(window).scroll(function () {
        var top = $(document).scrollTop();
        $('.splash').css({
            'background-position': '0px -' + (top / 3).toFixed(2) + 'px'
        });
        if (top > 50)
            $('#home > .navbar').removeClass('navbar-transparent');
        else
            $('#home > .navbar').addClass('navbar-transparent');
    });
})();

function setupCountDownScoutsAoVivo(urlResults, urlJogo) {
    $(".list-group-item").addClass("flash");

    $('#spanCountDown').countdown(getNovaData(), function (event) {
        $(this).html(event.strftime('%S')); //event.strftime('%D days %H:%M:%S')
    })
    .on('finish.countdown', function (event) {
        $("#divLoading").css("display", "block");

        $("#listaJogos").load(urlResults);
        $("#divConfronto").load(urlJogo + "/" + window.jogoCorrente);

        $("#divLoading").css("display", "none");
    });

    function getNovaData() {
        var data = new Date(); data.setSeconds(data.getSeconds() + 30);
        return data;
    }
}