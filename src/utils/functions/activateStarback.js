
const options = {
    type: 'dot',
        quantity: 100,
        direction: 225,
        width: 100,
        height:820,
        backgroundColor: ['#6ebfda', '#6ebfda'],
        randomOpacity: true,
}
export default (canvas) => {

    options.width = document.body.offsetWidth;
    new Starback(canvas, options)
    window.addEventListener('resize', ()=>{
        options.width = document.body.offsetWidth;
        new Starback(canvas, options)
    })
}