<template>
  <CollectionManager :modules="[
                         employee.create.includes(param) ? 'create' : '',
                         employee.select.includes(param) ? 'select' : '',
                         employee.delete.includes(param) ? 'delete' : '',
                         employee.update.includes(param) ? 'update' :'',
                    ]"
                     :key="employee"
                     :owner="employee"
                     :owner-key="ownerKey"
                     :emitter="emitter"
                     :set-collection="_setCollection"
                     :collection-key="param"
                     :collection="employee[param]"></CollectionManager>

</template>

<script>
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import {ClassBunch, LifeBunch} from "@/core/Bunch";
export default {
  components: {CollectionManager},
  props:{
    param:undefined,
    ownerId:undefined,
    ownerKey:undefined,
    setCollection:undefined,
    services:undefined,
    crud:undefined,
    toast:undefined,
    emitter:undefined,
    xpopupOptions:undefined,
  },
  data() {
    return {
      employee: new ClassBunch[this.ownerKey],
      life: new LifeBunch[this.ownerKey](),
      popupOptions: new this.xpopupOptions(this.$router, this.update),
    }
  },
  async mounted(){
    console.log('ownerKey', this.ownerKey)
    this.employee = await this.crud.getById(this.ownerId)
  },
  methods:{
    update(){
      this.crud.update(this.employee).then(res=>{
        if(res?.invalid){
          console.log('invalid employee create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },

    lock(param){
      if(!this.employee.$lock[param]){
        return false;
      }
      return this.employee.$lock[param]()
    },
    hide(param){
      if(!this.employee.$hide[param]){
        return false;
      }
      return this.employee.$hide[param]()
    },
    _setCollection(collection, key){
     this.setCollection(collection, key)
    }
  }
}
</script>



<style>

</style>