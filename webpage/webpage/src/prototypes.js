export default () =>{
    String.prototype.isNullOrEmpty = function() {
        return this === null || this === undefined || !this || this.trim() === '';
    };
}