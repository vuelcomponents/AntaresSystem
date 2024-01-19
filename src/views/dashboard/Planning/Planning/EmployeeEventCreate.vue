<script>
import {Planner} from "@/views/dashboard/Planning/Planning/Planner";
import {Plan} from "@/views/dashboard/Planning/Planning/Plan";
import {RowNode} from "ag-grid-community";

export default {
  props:{
    planner:typeof Planner,
    employee:typeof RowNode,
    employees:Array,
    services:undefined,
  },
  data(){
    return{
      plan:new Plan({employee:this.employee}),
      companies:[]
    }
  },
  mounted(){
    this.services.cmp.getAll().then(res=>{
      if(res.successful()){
        this.companies = res.data
      }
    })
  },
  methods:{
    create(){
      this.planner.create(this.plan).then(res=>{
        console.log('res', res)
      })
    }
  }
}
</script>

<template>
  <div class="on-blur-back" @click.self="()=>{planner.setVisibility(false); console.log(planner.show)}">
    <div class="on-blur-front">
      <input-text class="form-text-input" v-model="plan.title" placeholder="Title"></input-text>
      <Editor class="form-text-input" v-model="plan.content" placeholder="Content"></Editor>
      <Calendar date-format="yy-mm-dd" id="calendar-24h" v-model="plan.start" placeholder="start" showTime hourFormat="24" />
      <Calendar date-format="yy-mm-dd" id="calendar-24h" v-model="plan.stop" placeholder="stop" showTime hourFormat="24" />
      <DropDown :options="this.companies" v-model="plan.company" />
      <button @click="create">Save</button>
    </div>
  </div>

</template>

<style>
.on-blur-back{
  position:fixed;
  top:0;
  left:0;
  width:100vw;
  height:100vh;
  backdrop-filter: blur(10px);
  z-index:50;
  display:flex;justify-content:center;align-items:center;
  .on-blur-front{
    min-width:60vw;
    min-height:60vh;
    background: #f1f1f1;
    border-radius:16px;
  }
}
</style>