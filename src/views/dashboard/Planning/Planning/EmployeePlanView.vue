<script>
import VueCal from 'vue-cal'
import 'vue-cal/dist/vuecal.css'
import {EmployeeService} from "@/services/EmployeeService";
import Dropdown from "primevue/dropdown";
import {PlanService} from "@/services/PlanService";
import {Planner} from "@/views/dashboard/Planning/Planning/Planner";
import {inject, ref} from "vue";
import Menubar from "primevue/menubar";
import EmployeeEventCreate from "@/views/dashboard/Planning/Planning/EmployeeEventCreate.vue";

export default{
  components:{Menubar, VueCal, Dropdown, EmployeeEventCreate},
  props:{
    services:undefined,
  },
  setup(){
    const emitter = inject('emitter');
    return{
      emitter,
    }
  },
  data(){
    return{
      employees:[],
      selected:undefined,
      planner:new Planner(this.services.plan),
      navigation:[
        {
          label:'Create new Plan',
          command:()=>{(this.planner.setVisibility(true))}
        }
      ]
    }
  },
  mounted(){
    this.services.emp.getAll().then(res=>{
      if(res.successful()){
        this.employees = res.data
        console.log('employees', this.employees)
      }
    })
  },
  watch:{
    selected(){
      if(this.selected.id){
        this.services.plan.getPlanToEmployee(this.selected).then(res=>{
          if(res.successful()){
            this.selected.plans = res.data
            console.log('employee plans', res.data)
          }
        })
      }
    }
  }
}
</script>

<template>
   <EmployeeEventCreate :services="services" :employee="selected" :planner="planner" v-if="planner.show"/>
   <Dropdown v-model="selected" editable :options="employees"
             :optionLabel="(params)=>`${params.firstName} ${params.lastName}`"
             placeholder="Select employee"
             class="w-full md:w-14rem" />
    <Menubar class="on-dark-bg flex-left noradius" :model="navigation" />
    <vue-cal :time-from="8 * 60"
             :time-to="19 * 60"
             :events="planner.events"
             :time-step="30"/>
</template>

<style scoped>

</style>