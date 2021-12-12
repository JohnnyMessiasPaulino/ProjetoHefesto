function apagarFazenda(id) {
    if (confirm('Deseja realmente apagar este registro?'))
        location.href = "Index";
}

function apagarSensor(id) {
    if (confirm('Deseja realmente apagar este registro?'))
        location.href = "Delete?id=" + id;
}

function apagarProprietario(id) {
    if (confirm('Deseja realmente apagar este registro?'))
        location.href = "Index";
}


function aplicaFiltroConsultaAvancada() {
    var fazendaNome = document.getElementById('fazendaNome').value;
    var tamanhoInicial = document.getElementById('tamanhoInicial').value;
    var tamanhoFinal = document.getElementById('tamanhoFinal').value;
    var numeroSensores = document.getElementById('numeroSensores').value;

    var linkAPI = "ObtemDadosConsultaAvancada?fazendaNome=" + fazendaNome +
                        "&tamanhoInicial=" + tamanhoInicial +
                        "&tamanhoFinal=" + tamanhoFinal +
                        "&numeroSensores=" + numeroSensores;


    $.ajax({
        url: linkAPI,
        //data: { fazendaNome = vFazendaNome, tamanhoInicial: vTamanhoInicial, tamanhoFinal = vTamanhoFinal, numeroSensores : vnumeroSensores },
       
         success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
    });
}



function aplicaFiltroConsultaSensor(){
    var fazendaID = document.getElementById('fazendaID').value;
    var localizacao = document.getElementById('localizacao').value;

    var linkAPIsensor = "ObtemDadosConsultaSensor?fazendaID=" + fazendaID +
        "&localizacao=" + localizacao;

    $.ajax({
        url: linkAPIsensor,
       
         success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsultaSensor').innerHTML = dados;
            }
        },
    });
}

function aplicaFiltroConsultaMonitoramento() {
    var nomeFazenda = document.getElementById('nomeFazenda').value;
    var Id = document.getElementById('Id').value;

    linkAPImonitoramento = "ObtemDadosConsultaMonitoramento?nomeFazenda=" + nomeFazenda +
        "&Id=" + Id;

    $.ajax({
        url: linkAPImonitoramento,

        success: function (dados) {
            if (dados.erro != undefined) {
                alert(dados.msg);
            }
            else {
                document.getElementById('resultadoConsultaMonitoramento').innerHTML = dados;
            }
        },
    });
}
