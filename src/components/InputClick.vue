<script>
export default {
  props:{
    value:undefined,
    format:undefined,
  },
  methods:{
    $format(){
      if(!this.value){
        return 'null'
      }
      switch(this.format){
        case 'collection':
          return this.value?.length ?? 'empty array'
        default:
          return this.value;
      }
    }
  }
}
</script>

<template>
<div class="form-text-input pointer clicker">
  <i class="mdi mdi-link-box-variant"></i>
  {{$format()}}
</div>
</template>

<style>
.pointer{
  cursor:pointer;
}
.clicker{
  transition:all 0.6s ease;
  color:#16acc2;
  border:solid 2px #16acc2
}
.clicker:hover{
  color:#fff;
  background:#16acc2;
}
</style>