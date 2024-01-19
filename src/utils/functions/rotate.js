export default (el) => {
    el.classList.add('rotate')
    setTimeout(()=>{
        el.classList.remove('rotate')
    },400)
}