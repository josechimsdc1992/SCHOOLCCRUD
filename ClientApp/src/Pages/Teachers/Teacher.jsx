import axios from "axios";
import { Button } from "primereact/button";
import { Column } from "primereact/column";
import { ConfirmDialog } from "primereact/confirmdialog";
import { DataTable } from "primereact/datatable";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { apiListTeacher, apiTeacher } from "../../utils/ApiConfig";
import { Bounce, toast, ToastContainer } from "react-toastify";


function Teacher(){
    const navigate = useNavigate();
    
    const [teacher, setTeacher] = useState(null);
    const [selectedTeacher, setSelectedTeacher] = useState(null);
    const [visible, setVisible] = useState(null);

    useEffect(() => {
        loadData()
    }, []);

    const loadData=()=>{
        axios.get(`${apiListTeacher}`).then((response) => {
            setTeacher(response.data.result);
            });
    }

    const handleDelete=(row)=>{
        axios.delete(`${apiTeacher}/${row.idTeacher}`)
        .then((response) => {
            if(!response.data.hasError){
                loadData()
                alertSuccess('Data deleted')
            }else{
                alertWarning(response.data.mensaje);
            }
            
            })
            .catch((res)=>{
                alertWarning(res.response.data.mensaje);
            });
    }
    const handleEdit=(row)=>{
        if(row){
            navigate(`/teacher/${row.idTeacher}`);
        }
    }
    const handleNew=()=>{
        navigate(`/teacher/0`);
    }
    const alertSuccess= async (text) => {
        toast.success(text, {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
            }
            );
        
    };

    const alertWarning= async (text) => {
        toast.warn(text, {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
            });
    
        
    };

    
return(
    <section className="page-section" id="teacher">
            <div className="container">
                <div className="text-center">
                    <h2 className="section-heading text-uppercase">Teacher</h2>
                    <h3 className="section-subheading text-muted">Lorem ipsum dolor sit amet consectetur.</h3>
                </div>
                <div className="row text-center">
                                <div className="col-md-4 text-left">
                                <   Button onClick={() => handleNew()} label="New" severity="success" />
                                        
                                </div>
                                
                                { teacher ? 
                                <DataTable value={teacher} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} selectionMode="single"  selection={selectedTeacher} onSelectionChange={(e) => setSelectedTeacher(e.value)} tableStyle={{ minWidth: '60rem' }}>
                                <Column field="name" header="Name"></Column>
                                <Column field="surName" header="SurName"></Column>
                                <Column field="genero" header="Genero"></Column>
                                <Column header="" body={(row) => (
                                    <>
                                    
                                    <ConfirmDialog group="declarative"  visible={visible} onHide={() => setVisible(false)} message="Are you sure you want to delete?" 
                                        header="Confirmation" icon="pi pi-exclamation-triangle" accept={() => handleDelete(row) }  />
                                    <Button onClick={() => handleEdit(row)} icon="pi pi-check" label="Edit" severity="warning"/>
                                    <Button onClick={() => setVisible(true)} icon="pi pi-check" label="Delete" severity="danger"/>
                                    </>
                                    )}>
                                </Column>
                                </DataTable>
                                : <div></div>}
                                </div>
            </div>
            <ToastContainer />
        </section>
)
}
export default Teacher