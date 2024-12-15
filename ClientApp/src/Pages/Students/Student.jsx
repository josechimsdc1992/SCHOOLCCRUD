import axios from "axios";
import { Button } from "primereact/button";
import { Column } from "primereact/column";
import { ConfirmDialog } from "primereact/confirmdialog";
import { DataTable } from "primereact/datatable";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { Bounce, toast, ToastContainer } from "react-toastify";
import { apiListStudent, apiStudent } from "../../utils/ApiConfig";

function Student(){
    const navigate = useNavigate();
    
    const [students, setStudents] = useState(null);
    const [selectedStudent, setSelectedStudent] = useState(null);
    const [visible, setVisible] = useState(null);

    useEffect(() => {
        loadData()
    }, []);

    const loadData=()=>{
        axios.get(`${apiListStudent}`).then((response) => {
            setStudents(response.data.result);
            });
    }

    const handleDelete=(row)=>{
        axios.delete(`${apiStudent}/${row.idStudent}`).then((response) => {
            if(!response.data.hasError){
                loadData()
                alertSuccess('Data deleted')
            }else{
                console.log(response.data.mensaje)
            }
            
            });
    }
    const handleEdit=(row)=>{
        if(row){
            navigate(`/student/${row.idStudent}`);
        }
    }
    const handleNew=()=>{
        navigate(`/student/0`);
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

return(
    <section className="page-section" id="services">
            <div className="container">
                <div className="text-center">
                    <h2 className="section-heading text-uppercase">Student</h2>
                    <h3 className="section-subheading text-muted">Lorem ipsum dolor sit amet consectetur.</h3>
                </div>
                <div className="row text-center">
                <div className="col-md-4 text-left">
                <Button onClick={() => handleNew()} label="New" severity="success" />
                        
                </div>
                
                { students ? 
                <DataTable value={students} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} selectionMode="single"  selection={selectedStudent} onSelectionChange={(e) => setSelectedStudent(e.value)} tableStyle={{ minWidth: '60rem' }}>
                <Column field="name" header="Name"></Column>
                <Column field="surName" header="SurName"></Column>
                <Column field="genero" header="Genero"></Column>
                <Column field="date" header="Date"></Column>
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
export default Student