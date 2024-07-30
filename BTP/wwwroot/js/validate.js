function validateUsername() {
  var usernameInput = document.getElementById("username").value;
  var usernameError = document.getElementById("username-error");
  var usernameRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  usernameError.textContent = "";  // Réinitialise le message d'erreur

  if (!usernameRegex.test(usernameInput)) {
    usernameError.textContent = "Le username doit être au format email (exemple@domaine.com)";
    return false;
  }
  return true;
}

// Fonction pour valider le champ "password"
function validatePassword() {
  var passwordInput = document.getElementById("password").value;
  var passwordError = document.getElementById("password-error");
  var passwordRegex = /^(?=.*[A-Z])(?=.*\d).{6,}$/;
  passwordError.textContent = "";  // Réinitialise le message d'erreur

  if (!passwordRegex.test(passwordInput)) {
    passwordError.textContent = "Le password doit contenir au moins 6 caractères, une lettre majuscule et un chiffre";
    return false;
  }
  return true;
}

// Fonction pour valider le formulaire complet
function validateForm() {
  var isUsernameValid = validateUsername();
  var isPasswordValid = validatePassword();
  return isUsernameValid && isPasswordValid;
}