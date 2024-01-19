<script>

import EmployeePlanView from "@/views/dashboard/Planning/Planning/EmployeePlanView.vue";
import CompanyPlanView from "@/views/dashboard/Planning/Planning/CompanyPlanView.vue";
import TabMenu from "primevue/tabmenu";
import SharkTabMenu from "@/components/SharkTabMenu.vue";
import {inject} from "vue";
import {EmployeeService} from "@/services/EmployeeService";
import Toast from "@/utils/classes/Toast";
import {CompanyService} from "@/services/CompanyService";
import {PlanService} from "@/services/PlanService";

export default{
  components:{EmployeePlanView, CompanyPlanView, SharkTabMenu},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp:new EmployeeService(emitter),
      cmp:new CompanyService(emitter),
      plan: new PlanService(emitter)
    };
    const toast = new Toast()
    return {
      services,
      emitter,
      toast
    }
  },
  data(){
    return{
      mode:'employee',
      navigation : [
        {
          label:'Company Plan',
          command:()=>{(this.mode = 'company')}
        },
        {
          label:'Employee plan',
          command:()=>{(this.mode = 'employee')}
        }
      ]
    }
  }
}
</script>

<template>
<main class="black flex-left">
  <SharkTabMenu :items="navigation" />
  <EmployeePlanView :services="services" v-if="mode==='employee'"/>
  <CompanyPlanView :company-service="services.cmp" :plan-service="services.plan" v-if="mode==='company'"/>
</main>
</template>

<style>
.black{
  color:black;
}
.flex-left{
  display:flex;
  justify-content:flex-start;
  flex-direction:column;
}
</style>