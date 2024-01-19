<script>
export default{
props:{
    close:Function,
    noFixed:undefined,
  },
  data(){
    return{
      isFullScreen:false
    }
  },
  methods:{
    closePopup(event){
      if(event.target.id !== 'flex-data-popup-container'){
        return;
      }
      this.close()
    },
    fullScreen(){
      const doc = document.querySelector('html');
      this.isFullScreen = !this.isFullScreen
      if(this.isFullScreen){
        doc.classList.add('unsetOverflow')
      }else{
        doc.classList.remove('unsetOverflow')
      }
    }
  }
}
</script>

<template>
<div v-if="!noFixed" :class="{'flex-data-popup-container':true, 'containerFullscreen':isFullScreen }" ref="popupContainer" @click="closePopup" id="flex-data-popup-container">
  <div :class="{'flex-data-popup':true, 'fullscreen':isFullScreen }" ref="popup">
    <div class="toolbar">
      <span @click="fullScreen">
        <i class="mdi mdi-fullscreen"></i>
      </span>
    </div>
    <div>
        <slot name="content" />
    </div>
  </div>
</div>
  <div v-if="noFixed" >
      <div>
        <slot name="content" />
      </div>
  </div>
</template>

<style>
.flex-data-popup-container{
  width:100vw;
  height:100vh;
  position:fixed;
  z-index:20;
  left:0;
  top:0;
  background:#00000030;
  display:flex;
  justify-content:center;

  align-items:center;
  .flex-data-popup{
    background:#fff;
    width:60vw;
    border:solid 3px #fff;
    border-radius:8px;
    height:400px;
    overflow:scroll;
    margin-top: 20vh;/* Ustaw margines górny na połowę wysokości elementu */
    .toolbar{
      width:100%;
      height:40px;
      background: #011113;
      color:#fff;
      display:flex;
      padding-inline:15px;
      align-items:center;
      span{
        width:35px;
        height:35px;
        display:flex;
        align-items:center;
        justify-content:center;
        border-radius:4px;
        cursor:pointer;
      }
      span:hover{
        background:#fff;
        color:black
      }
    }
  }
}
.overflow{
  overflow:auto;
}
.fullscreen {
  width: 100vw!important;
  height: calc(100vh - 70px)!important;
  margin-top: 70px!important;
  border: none!important;
  overflow-y:scroll!important;
}
.containerFullscreen{
  display:flex;
  justify-content:center;
  align-items:flex-start!important;
}
.fullscreen.flex-data-popup {
  border-radius:0!important;
}
.unsetOverflow{
  overflow-y:hidden!important;
}
</style>