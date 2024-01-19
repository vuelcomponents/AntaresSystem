<template>
<router-view/>
</template>

<script>
export default {
    name: "DataView",
    mounted(){

    }
}
</script>

<style>
.func-footer{
    position:fixed;
    bottom:0;
    right:0;
    width:100%;
    height:30px;
    background: #566981;
    border-top: solid 2px #051f2240;
    z-index:8;
    transition: all 0.4s ease;
    transform:translateY(35px);
}
.translateY35{
    transform:translateY(35px);
}
.translateY0{
    transform:translateY(0)!important;
}
.form-text-select{
  min-width:200px
}
</style>