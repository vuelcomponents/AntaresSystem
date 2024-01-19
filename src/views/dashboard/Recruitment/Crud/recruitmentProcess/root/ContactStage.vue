<script >
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";
import Rating from "primevue/rating";

export default{
  name:"ContactStage",
  components:{Rating},
  props:{
    employee:undefined,
    close:Function
  },
  data(){
    return{
      recruitmentContact:this.employee.recruitmentContact,
    }
  },
  methods:{

  }
}
</script>

<template>
  <seciton class="stage-container" id="contact-stage-container" @click="close">
    <main style="diisplay:flex;flex-direction:column;">

      <p>Time and date of meeting</p>
      <Calendar class="form-text-input" v-model="recruitmentContact.dateTime" date-format="yy-mm-dd"></Calendar>
      <p>Link to meeting</p>
      <InputText class="form-text-input" v-model="recruitmentContact.link" ></InputText>
      {{employee.recruitmentContact}}
      <Button>Send invite</Button>
      <p>Is the contact accomplished?</p>
      <input type="checkbox" v-model="recruitmentContact.done">
      <p>Rating</p>
      <Rating :disabled="!recruitmentContact.done" v-model="recruitmentContact.rating" :cancel="false" />

    </main>
  </seciton>

</template>

<style >

</style>
