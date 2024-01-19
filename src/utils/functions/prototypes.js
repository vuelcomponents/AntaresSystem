export default () => {
    Array.prototype.stringify = function() {
        var string =  JSON.stringify(this)
            .replaceAll("[","")
            .replaceAll("]","")
            .replaceAll(",",", ")
            .replaceAll("\"","")
            .substring(0, 200);
        if(string.length>199) string +='...'
        return string;
    };

    Object.defineProperty(Object.prototype, 'successful', {
        value: function() {
            return this.status === 200 && this.data;
        },
        enumerable: false,
    });
}
