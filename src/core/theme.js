const setTheme = (theme) =>{
    localStorage.setItem('theme', theme)
    getTheme()
}

const getThemeName=()=>{
    var themeName = localStorage.getItem('theme')
    if(!themeName || themeName === ''){
        themeName = 'casual'
    }
    return themeName
}
const getTheme = ()=>{
    const theme = localStorage.getItem('theme')
    const body = document.body;
    switch(theme){
        case 'lightTheme':
            if(body.classList.contains('_redShark')){
                body.classList.remove('_redShark')
            }
                if(!body.classList.contains('_lightTheme')) {
                    body.classList.add('_lightTheme')
                }
            break;
        case 'redShark':
            if(body.classList.contains('_lightTheme')){
                body.classList.remove('_lightTheme')
            }
            if(!body.classList.contains('_redShark')) {
                body.classList.add('_redShark')
            }
            break;
        default:
                if(body.classList.contains('_lightTheme')){
                    body.classList.remove('_lightTheme')
                }
            if(body.classList.contains('_redShark')){
                body.classList.remove('_redShark')
            }
            break;
    }
}

export {getTheme, setTheme, getThemeName}