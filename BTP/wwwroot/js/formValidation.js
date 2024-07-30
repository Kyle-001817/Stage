$(document).ready(function () {
    $('form').submit(function (event) {
        event.preventDefault();

        var form = $(this);
        var controller = form.data('controller');
        var action = form.data('action');

        $.ajax({
            url: '/' + controller + '/' + action,
            type: 'POST',
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    if (response.redirectUrl) {
                        window.location.href = response.redirectUrl;
                    } else {
                        alert('Validation réussie');
                        form[0].reset();
                    }
                } else {
                    var errorMessages = '';
                    $.each(response.errors, function (key, value) {
                        errorMessages += '<p>' + value + '</p>';
                    });

                    $('#errorModal .modal-body').html(errorMessages);
                    $('#errorModal').modal('show');

                    // Ferme la modale après 3 secondes
                    setTimeout(function () {
                        $('#errorModal').modal('hide');
                    }, 3000);
                }
            },
            error: function (xhr, status, error) {
                // Gestion des erreurs de requête AJAX
                var errorMessage = '<p>Une erreur est survenue. Veuillez réessayer plus tard.</p>';
                $('#errorModal .modal-body').html(errorMessage);
                $('#errorModal').modal('show');

                // Ferme la modale après 3 secondes
                setTimeout(function () {
                    $('#errorModal').modal('hide');
                }, 3000);
            }
        });
    });
});
