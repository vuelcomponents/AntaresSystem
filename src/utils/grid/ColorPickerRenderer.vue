<script >
export default{
props:{
  params:undefined
},
  data(){
  return{
    color:this.params?.data?.color
  }
  },
  watch:{
    color(){
      this.params.setValue(this.color)
    }
  }
}
</script>

<template>
<div>
  <ColorPicker  v-model="color"></ColorPicker>
</div>
</template>

<style>

</style>