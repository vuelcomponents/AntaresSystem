var menuHide = false;
export default () =>{
    const container = (document.getElementById("dashboard-container"))
    const menu =(document.getElementById("left-dashboard-unique"))
    if(!menuHide){
        container.style.gridTemplateColumns="1fr"
        menu.style.display="none"
    }else{
        container.style.gridTemplateColumns="1fr 4.5fr"
        menu.style.display="inherit"
    }
    menuHide = !menuHide




}