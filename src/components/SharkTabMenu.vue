<script >
export default{
  props:{
    items:Array
  },
  data(){
    return{
      //
    }
  },
  mounted(){
    //
  },
  methods:{
    //
  }
}
</script>

<template>
  <nav class="shark-tab-menu">
    <button v-for="item in items" @click="item.command" :key="item">{{item.label}}</button>
  </nav>
</template>

<style >

</style>