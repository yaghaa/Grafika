var Plik = function(data) {
    flaga1: data.flaga1();
    flaga2: data.flaga2();
}

var Baza = function(x) {
    x = x || {}
    flaga3: x.flaga3();
    flaga4: x.flaga4();
}

var Object1 = function() {
    var plik = new Plik(data);
    var baza = new Baza();

    clientId: data.clientId();
}

var Object2 = function() {
    clientId: data.clientId();
    cos: data.cos();

    flaga3: data.flaga3();
    flaga4: data.flaga4();
};

var merge = function() {
    var listaPlik = [new Object1(), new Object1()];
    var listaBaza = [new Object2(), new Object2()];
    
    listaPlik.forEach(function (item) {
        var baseItem = Enumerable.From(listaBaza).Where(function (x) { return x.clientId = item.clientId });
        item.baza = new Baza(baseItem);
    });
        
    
}