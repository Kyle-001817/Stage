
document.getElementById('datetime-seconds').addEventListener('input', function (e) {
var value = e.target.value;
var regex = /^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$/;

if (!regex.test(value)) {
e.target.setCustomValidity("Le format doit être YYYY-MM-DD HH:MM:SS");
} else {
e.target.setCustomValidity("");
}
});