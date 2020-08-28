$(document).ready(function () {
    $("#msg_box").fadeOut(5000);

    $("#tableIndex").DataTable({
        "language": {
            "lengthMenu": "Exibir _MENU_ registros por página",
            "zeroRecords": "Nenhum registro encontrado",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Nenhum registro encontrado",
            "infoFiltered": "(filtro aplicado em _MAX_ registros)",
            "search": "Pesquisar"
        },
        //"paging": false,
        //"info": false
        "lengthMenu": [[20, 40, 60, 80, -1], [20, 40, 60, 80, "All"]]
    });

    setNavigation();
});

function setNavigation() {
    if (sessionStorage.getItem("activeMenuItemID") != undefined) {
        $("#menu-content > li").each(function () {
            if ($(this).attr("id") == sessionStorage.getItem("activeMenuItemID")) {
                setNavigationMenuItem(this);
            }
        });

        $("#menu-content > ul > li").each(function () {
            if ($(this).attr("id") == sessionStorage.getItem("activeMenuItemID")) {
                setNavigationMenuItem(this);
            }
        });
    }

    $('#menu-content > li').click(function () {
        setNavigationMenuItem(this);
    });

    $('#menu-content > ul > li').click(function () {
        setNavigationMenuItem(this);
    });

    $('#logomarca').click(function () {
        sessionStorage.removeItem("activeMenuItemID");
    });
}

function setNavigationMenuItem(elem) {
    if ($(elem).attr('class') != 'active') {
        $('#menu-content > li').removeClass("active");
        $('#menu-content > ul > li').removeClass("active");
        $(elem).addClass("active");

        $(elem).parent('.sub-menu').removeClass('collapse');
        $(elem).parent('.sub-menu').addClass('collapsed');

        sessionStorage.setItem("activeMenuItemID", $(elem).attr("id"));
    }
}

function AjaxModal() {
    $(function () {
        $.ajaxSetup({ cache: false });

        $("a[data-modal]").on("click",
            function (e) {
                $('#myModalContent').load(this.href,
                    function () {
                        $('#myModal').modal({
                            keyboard: true
                        },
                            'show');
                        bindForm(this);
                    });
                return false;
            });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#ElementTarget').load(result.url);
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });
        return false;
    });
}

function AjaxModalReverso() {
    $(function () {
        $.ajaxSetup({ cache: false });

        $("a[data-modal-reverso]").on("click",
            function (e) {
                $('#myModalContent').load(this.href,
                    function () {
                        $('#myModal').modal({
                            keyboard: true
                        },
                            'show');
                        bindFormReverso(this);
                    });
                return false;
            });
    });
}

function bindFormReverso(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#myModalContent').html(result);
                } else {
                    $('#myModalContent').html(result);
                    bindFormReverso(dialog);
                }
            }
        });
        return false;
    });
}