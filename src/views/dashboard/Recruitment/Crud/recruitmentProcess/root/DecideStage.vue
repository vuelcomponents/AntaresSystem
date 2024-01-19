<script >
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";

export default{
  name:"DecideStage",
  props:{
    employee:undefined,
    close:Function
  },
  methods:{

  }
}
</script>

<template>
  <seciton class="stage-container" id="decide-stage-container" @click="close">
    <main>
      {{employee}}
    </main>
  </seciton>

</template>

<style >

</style>
