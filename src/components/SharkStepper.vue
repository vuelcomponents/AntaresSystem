<script >
export default{
  props:{
    items:Array,
    stepChange:Function
  },
  data(){
    return {
      state:null
    }
  },
  methods:{
    setState(state){
      this.state = state
    }
  },
  mounted(){
    if(this.items[0]?.id){
      this.state = this.items[0].id
    }
  },
  watch:{
    state(){
      this.stepChange(this.state)
    }
  }
}
</script>

<template>
<section class="shark-stepper-header">
  <header v-for="item in items" @click="setState(item)" :key="item">
    <span><i :class="`mdi ${item.mdi}`"/></span>
    <span>{{item.title}}</span>
  </header>
</section>
<section class="shark-stepper-window">
  <div  v-for="item in items" :key="item" :hidden="this.state !== item.id">
    <slot :name="item.id">

    </slot>
  </div>

</section>
</template>

<style>
.shark-stepper-header{
  display:flex;
  align-items:center;
  max-width:100%;
  box-sizing:border-box;
  justify-content:flex-start;
  gap:10px;
  background:#001c20;
  *{
    color:#fff;
  }
  header{
    min-width:120px;
    padding:8px;
    user-select:none;
    cursor:pointer;
    transition:all 0.7s ease
  }
  header:hover{
    color:#fff;
    background: #261fa4;
    border-radius:2px;
  }
}
.shark-stepper-window{
  box-shadow: 0px -5px 5px -5px rgba(0, 0, 0, 0.15);
}

</style>