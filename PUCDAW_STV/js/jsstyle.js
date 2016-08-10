//Ativar o toogle ao clicar no header do panel.
$('.expandir').click(function () {
    $(this, '.panel-collapse').next().collapse('toggle');
    $(this).find('span.toggle-icon').toggleClass('glyphicon-minus glyphicon-plus');
});