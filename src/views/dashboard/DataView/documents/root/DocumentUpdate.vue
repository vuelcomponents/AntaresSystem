<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in xdocument.getInputs('text')" :key="param">
          <p class="form-label">
            {{xdocument.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="xdocument[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in xdocument.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="xdocument.getHeaders([param],'first')" class="form-text-input" v-model="xdocument[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in xdocument.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <Calendar :disabled="lock(param)" date-format="yy-mm-dd"  v-model="xdocument[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in xdocument.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="xdocument[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   xdocument.create.includes(param) ? 'create' : '',
                                   xdocument.select.includes(param) ? 'select' : '',
                                   xdocument.delete.includes(param) ? 'delete' : '',
                                   xdocument.update.includes(param) ? 'update' :'',
                              ]"
                               :owner="xdocument"
                               owner-key="document"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="xdocument[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in xdocument.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="xdocument" :param="param" :disabled="lock(param)" :model="xdocument"></SelectionManager>
          </div>
        </div>
        <div v-for="param in xdocument.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{xdocument.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="xdocument.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="xdocument.setFile($event, param)" />
          </div>
          <Tag v-if="xdocument.fileData"
               severity="warning"
               style="margin-top:4px;cursor:pointer;"
               @click="this.services.doc.download(this.$route.params.id, xdocument.fileName)"
          >Download</Tag>
        </div>
      </template>
      <template v-slot:item2contentLeft>
        <div class="box">
          {{ xdocument }}
        </div>
      </template>

      <template v-slot:item2contentRight>
        <div class="box">
          right
        </div>
      </template>

      <template v-slot:item3contentLeft>
        <div class="box">
          left
        </div>
      </template>

      <template v-slot:item3contentRight>
        <div class="box">
          right
        </div>
      </template>
    </DataPopup>
  </div>
</template>

<script>

import DataPopup from "@/components/popups/DataPopup.vue";
import EditOptions from "@/views/dashboard/DataView/documents/options/EditOptions";
import { inject } from "vue";
import {DocumentService} from "@/services/DocumentService";
import DocumentCrudController from "@/views/dashboard/DataView/documents/controllers/DocumentCrudController";
import Toast from "@/utils/classes/Toast";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import RealisationService from "@/services/RealisationService";
import DocumentLife from "@/views/dashboard/DataView/documents/DocumentLife";
import DocumentClass from "@/views/dashboard/DataView/DocumentClass";
import {EmployeeService} from "@/services/EmployeeService";
import {CompanyService} from "@/services/CompanyService";
import Tag from "primevue/tag";


export default {
  components: {CollectionManager, DataPopup, Tag},
  setup(){
    const emitter = inject('emitter')
    const services = {
      doc: new DocumentService(emitter),
      employee: new EmployeeService(emitter),
      company:new CompanyService(emitter),
      realisations:new RealisationService(emitter)
    };
    const toast =new Toast();
    const crud = new DocumentCrudController(services.doc, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      xdocument: new DocumentClass(),
      life: new DocumentLife(),
      popupOptions: new EditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.xdocument = await this.crud.getById(this.$route.params.id)
  },
  methods:{
    update(){
      this.crud.update(this.xdocument).then(res=>{
        if(res?.invalid){
          console.log('invalid document create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.xdocument[key] = collection
      }
    },
    lock(param){
      if(!this.xdocument.$lock[param]){
        return false;
      }
      return this.xdocument.$lock[param]()
    },
    hide(param){
      if(!this.xdocument.$hide[param]){
        return false;
      }
      return this.xdocument.$hide[param]()
    }
  }
}
</script>



<style>

</style>