$(() => {

      $("input").on('input', function () {
        ensureFormIsValid();

    });

 function ensureFormIsValid() {
        const email = $("#email").val().trim();
        const password = $("#password").val().trim();
         let isValid = email && password;
        $("#login").prop('disabled', !isValid);

    }

});