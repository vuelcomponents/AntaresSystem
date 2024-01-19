<template>
    <nav ref="navigation" id="nav" :style="{ height:'100vh',marginTop: marginTop + 'px',  transition:'none' }">

        <section  :style="`height:122x;`" class="transition" v-for="item in this.menu" :key="item" :ref="`section-${item.label}`">
            <div @click="dropDown(item)">
                <i :class=" item.icon ?? 'mdi mdi-link'"></i>
                {{item.label}}
            </div>
            <div v-for="sub in item.children" :key="sub" :class="['sublink', item.dropped ? null : 'hidden']" @click="()=>{if(sub.routeName){this.$router.push({name:sub.routeName})}}">
                <i :class="sub.icon ?? 'mdi mdi-link'"></i>
                {{sub.label}}
            </div>
            <div style="display:none;" class="hidden"></div>
        </section>

    </nav>


</template>

<script>

export default {
    name: "SideMenu",
    methods:{
      dropDown(item){
          item.dropped = !item.dropped;
          var ref=  this.$refs[`section-${item.label}`][0]
          if(item.dropped){
              ref.style.height= 130 + item.children.length * 38 + 'px'
          }
          else{
              ref.style.height= "100px"
          }
      },
      fitMenuSize(){
        setTimeout(()=>{
          if(this.$refs.navigation) {
            this.$refs.navigation.style.minWidth = this.$refs.navigation.parentElement.clientWidth + 'px'
            this.$refs.navigation.style.width = this.$refs.navigation.parentElement.clientWidth + 'px'
          }
        },100)

      }
    },
    data(){
        return {
            marginTop: 0,
            marginBottom: 0,
            pageSize:0,
            services:{

            },
            items:{
              Recruitment:[
                {
                  label:'Recruitment Process',
                  icon:'mdi mdi-account-multiple',
                  dropped:true,
                  children:[
                    {
                      label:'List',
                      routeName:'RecruitmentList'
                    },
                    {
                      label:'Create new recruitment',
                      routeName:'RecruitmentList'
                    },
                    {
                      label:'Processes history'
                    },
                    {
                      label:'Notifying'
                    },
                    {
                      label:'Email configuration'
                    },

                  ],
                },
              ],
                Management:[
                    {
                        label:'Employees',
                        icon:'mdi mdi-account-multiple',
                        dropped:false,
                        children:[
                            {
                                label:'List',
                                routeName:'EmployeeList'
                            },
                        ],
                    },
                    {
                        label:'Companies',
                        icon:'mdi mdi-domain',
                        dropped:false,
                        children:[
                            {
                              label:'List',
                              routeName:'CompanyList'
                            },
                            {
                                label:'Planning',
                                routeName:'Planning'
                            },
                            {
                                label:'Jobs',
                                routeName:'Jobs'
                            },
                        ]
                    },
                    {
                        label:'Store',
                        dropped:false,
                        icon:'mdi mdi-book',
                        children:[
                          {
                              label:'Variants',
                              routeName:'VariantList'
                          },
                          {
                            label:'Documents',
                            routeName:'DocumentList'
                          },
                          {
                            label:'Document types',
                            routeName:'DocumentTypeList'
                          },
                          {
                              label:'Status',
                              routeName:'StatusList'
                          },
                          {
                              label:'Actions',
                              routeName:'ActionFunctionList'
                          },
                          {
                            label:'Mails',
                            routeName:'MailList'
                          },
                          {
                            label:'Offers',
                            routeName:'OfferList'
                          },
                          {
                            label:'Houses',
                            routeName:'HouseList'
                          },
                          {
                            label:'Cars',
                            routeName:'CarList'
                          },
                        ]
                    },

                ],
                BonusSystem:[

                ],

            },
            module:null,
            menu:[],
        }
    },
    mounted(){
        this.menu = this.items[(this.$route.matched[1].name)];
        this.$nextTick(()=>{
          this.fitMenuSize()
        })
      window.addEventListener('resize', this.fitMenuSize)
      window.addEventListener('zoom', this.fitMenuSize)
    },


}
</script>

<style >
.hidden{
    display:none!important;
}


.height{
    height:40px;
}
.no-height{
    height:0;
}

 .sticky-navigation {
   position: sticky;
   top: 0;
   z-index: 9; /* Adjust the z-index as needed */
 }

</style>