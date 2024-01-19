<script>
import VueCal from 'vue-cal'
import 'vue-cal/dist/vuecal.css'
import {CompanyService} from "@/services/CompanyService";
import {PlanService} from "@/services/PlanService";
export default{
  components:{VueCal},
  props:{
    companyService:typeof CompanyService,
    planService:typeof PlanService
  },
  data(){
    return{

    }
  },
  mounted(){

  }
}
</script>

<template>
  <vue-cal :time-from="8 * 60"
           :time-to="19 * 60"
           :time-step="30"/>
</template>

<style>

</style>