/**
 * Classe récupérée du dossier FaceEdgeOrientation disponible sur le moodle
 */


//Références au Système
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//Références à SolidWorks
using SldWorks;
using SwConst;


namespace Wrapper
{
    // Ce code encapsule les appels à l'API de Solidworks. 
    // Il permet d'éviter de faire référence à l'API de Solidworks partout dans le code.
    // Ce n'est pas du code de production mais bien du code académique!
    // Par exemple, le traitement des exception est pas mal folklorique!
    public class SolidWorksWrapper
    {
        #region Connexion à SolidWorks, document, part, assembly, etc.
        // membres privés (pas accessibles directement à l'extérieur de la classe)
        private SldWorks.SldWorks mySWApp = null;
        //private ModelDoc2 activeDocument = null;
        //private int activeDocType = 0;

        // membres public 
        public object AppReference { get => (object)mySWApp; } // Entorse à l'encapsulation, donne un accès à un objet privé!
        // public SldWorks.SldWorks MySWApp { get => mySWApp; set => mySWApp = value; }
        //public int ActiveDocType { get => activeDocType; set => activeDocType = value; } // Encapsule activeDoc
        
        public object ActiveDocument 
        {
            //get => activeDocument;
            get
            {
                return (object) mySWApp.IActiveDoc2;
            }

            set 
            {
                ModelDoc2 docToSetActive = (ModelDoc2)value;
                string docToActivateFullPathName = (string)docToSetActive.GetPathName();
                string docToActivateName;
                int indexOfPoint = docToActivateFullPathName.LastIndexOf('\\');
                int swError = 0;
                if (indexOfPoint < 0) 
                    docToActivateName = docToActivateFullPathName;
                else
                    docToActivateName = docToActivateFullPathName.Remove(indexOfPoint);
                
                mySWApp.ActivateDoc3(docToActivateName, false, (int)swRebuildOnActivation_e.swRebuildActiveDoc, ref swError);
                // il faudrait traiter le cas des types d'erreur : swError
            }
        }
        public bool isActiveDocAPart()
        {
            ModelDoc2 activeDocument = mySWApp.IActiveDoc2;
            //PartDoc swPartDoc = null;
            if (activeDocument == null)
            {
                // TO DO
                return false;
            }
            else
            {
                if (activeDocument.GetType() == (int)swDocumentTypes_e.swDocPART)
                    return true;
                else
                    return false;
            }
        }
        
        public object GetPart()
        {
            // à appeler seulement lorsqu'on est sur d'avoir un active Part!
            return mySWApp.ActiveDoc; // return PartDoc qui est un objet
            // return (object) mySWApp.ActiveDoc; // retourne explicitement un object
        }

        public object GetBodies(object _part)
        {
            // TO DO
            return null;
        }
        public object GetFaces( object _part)
        {
            PartDoc swPart = (PartDoc)_part;
            // on suppose un seul body, sinon, il faut parcourir la collection de bodies!
            object[] objBodies = swPart.GetBodies2((int)swBodyType_e.swSolidBody, true);
            foreach (Body2 swBody in objBodies) // Hypothèse : il n'y en a qu'un!
            {
                object[] objFaces = swBody.GetFaces();
                return objFaces;
            }
            return null; // nedevrait pas arriver!
        }

         /// <summary>
        /// La fonction ConnexionSldworks() est appelé en premier lors de l'exécution de l'application
        /// Elle permet d'établir la connexion avec Solidworks
        /// </summary>
        /// <returns></returns>
        
        // Constructeur de la classe
        public SolidWorksWrapper()
        {
            mySWApp = connectToSldworks();
            if (mySWApp != null) mySWApp.Visible = true; // cette ligne pourrait être sortie de là!
        }
        private SldWorks.SldWorks connectToSldworks()
        {
            // Tente de se connecter à une instance existante ou de démarrer une instance de Solidworks
            SldWorks.SldWorks mySldworks;   //on crée une référence à une instance d'un objet de type SolidWorks
            try
            {
                mySldworks = new SldWorks.SldWorks();
                return mySldworks;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public object OpenSWFile()
        {
            // TO DO
            return null;
        }



        /// <summary>
        /// La fonction OuverturePart() permet de connecter la variable mySldW à un document actif de SolidWorks.
        /// Ce programme part du principe que l'on a déja ouvert une pièce dans SolidWorks
        /// </summary>
        /// <param name="mySldW"></param>
        /// <returns></returns>
        public static ModelDoc2 OpenModelDoc2(SldWorks.SldWorks mySldW)
        {
            /* Déclaration des variables interne*/
            ModelDoc2 doc = mySldW.ActiveDoc;

            /*Gestion du cas où aucun document n'est ouvert*/
            if (doc == null)
            {
                Console.WriteLine("Aucun document n'est ouvert");
            }

            /* Méthode permettant de vérifier que le document est un ASSEMBLY*/
            Debug.Print("Nom du fichier: " + doc.GetPathName());

            /*Gestion du cas où le document ouvert n'est pas un assemblage*/
            if (doc.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                Console.WriteLine("Le document actif n'est pas un assemblage. \n La prochaine fois, ouvrez un assemblage");
            }
            return doc; // Retourne le document contenant l'assemblage
        }
        #endregion

        #region Méthodes d'acces aux B-REP

        /// <summary>
        /// Cette fonction permet de récupérer tous les composants d'un assemblage (un composant peut être un 
        /// PartDoc ou un AssemblyDoc
        /// </summary>
        /// <param name="ADoc"></param>
        /// <returns></returns>
        public static Component2[] ExtractComponents(AssemblyDoc ADoc)
        {
            object[] SwComponents = null;
            SwComponents = (object[])ADoc.GetComponents(true);
            Component2[] SwComponentsReturn = new Component2[0];
            int i = 0;
            foreach (Component2 SwC in SwComponents)
            {
                Array.Resize(ref SwComponentsReturn, SwComponentsReturn.Length + 1);
                SwComponentsReturn[i] = SwC;
                i += 1;
            }
            return SwComponentsReturn;
        }

        /// <summary>
        /// Extraction du ModelDoc2 d'un composant
        /// </summary>
        /// <param name="AComponents"></param>
        /// <returns></returns>
        public static ModelDoc2 ExtractModelDoc2(Component2 AComponent)
        {
            ModelDoc2 SwModelDoc2;
            SwModelDoc2 = AComponent.GetModelDoc2();
            return SwModelDoc2;
        }

        /// <summary>
        /// Permet d'accéder aux ModelDoc2 d'un tableau de composants
        /// </summary>
        /// <param name="AComponents"></param>
        /// <returns></returns>
        public static ModelDoc2[] ExtractModelDoc2Tab(Component2[] AComponents)
        {
            ModelDoc2[] SwModelDoc2 = new ModelDoc2[0];
            int i = 0;
            foreach (Component2 SwComponent2 in AComponents)
            {
                Array.Resize(ref SwModelDoc2, SwModelDoc2.Length + 1);
                SwModelDoc2[i] = SwComponent2.GetModelDoc2();
                i += 1;
            }
            return SwModelDoc2;
        }

        /// <summary>
        /// Extraction du PartDoc d'un ModelDoc2
        /// </summary>
        /// <param name="AModelDoc2"></param>
        /// <returns></returns>
        public static PartDoc ExtractPartDoc(ModelDoc2 AModelDoc2)
        {
            PartDoc SwPart = new PartDoc();
            if (AModelDoc2.GetType() == (int)swDocumentTypes_e.swDocPART)
            {
                SwPart = (PartDoc)AModelDoc2;
            }
            return SwPart;
        }

        /// <summary>
        /// On ne sélectionne que les PartDoc parmis tous les ModelDoc2
        /// </summary>
        /// <param name="AModelDoc2"></param>
        /// <returns></returns>
        public static PartDoc[] ExtractPartDocTab(ModelDoc2[] AModelDoc2)
        {
            PartDoc[] SwPart = new PartDoc[0];
            int i = 0;
            foreach (ModelDoc2 SwModelDoc2 in AModelDoc2)
            {
                if (SwModelDoc2.GetType() == (int)swDocumentTypes_e.swDocPART)
                {
                    Array.Resize(ref SwPart, SwPart.Length + 1);
                    SwPart[i] = (PartDoc)SwModelDoc2;
                    i += 1;
                }
            }
            return SwPart;
        }

        /// <summary>
        /// Méthode d'extraction des PartDoc d'un partir d'un tableau de Component2
        /// </summary>
        /// <param name="SwComponents"></param>
        /// <returns></returns>
        public static PartDoc[] AccessPartDocTab(Component2[] SwComponents)
        {
            ModelDoc2[] SwModelDoc2 = (ModelDoc2[])ExtractModelDoc2Tab(SwComponents).Clone();
            PartDoc[] SwPart = (PartDoc[])ExtractPartDocTab(SwModelDoc2).Clone();
            return SwPart;
        }

        /// <summary>
        /// Méthode d'extraction du body d'un part. Ne fonctionne
        /// Que s'il n'y qu'un seul Body
        /// </summary>
        /// <param name="SwPartDoc"></param>
        /// <returns></returns>
        public static Body2 AccessBody(PartDoc SwPartDoc)
        {
            object[] bodies;
            Body2 SwBody2;
            bodies = SwPartDoc.GetBodies2((int)swBodyType_e.swSolidBody, false);
            if (bodies.Length > 1)
            {
                Console.WriteLine("Cette part a plusieurs Body");
            }
            SwBody2 = (Body2)bodies[0];
            return SwBody2;
        }

        /// <summary>
        /// Méthode d'accès aux Face2 d'un Body2
        /// </summary>
        /// <param name="SwBody2"></param>
        /// <returns></returns>
        public static Face2[] AccessFace2(Body2 SwBody2)
        {
            object[] faces;
            Face2[] SwFace2 = new Face2[0];
            faces = SwBody2.GetFaces();
            int i = 0;
            foreach (Face2 TempFace2 in faces)
            {
                Array.Resize(ref SwFace2, SwFace2.Length + 1);
                SwFace2[i] = TempFace2;
                i += 1;
            }
            return SwFace2;
        }

        /// <summary>
        /// Méthode d'accès aux Edge d'un Body2
        /// </summary>
        /// <param name="SwBody2"></param>
        /// <returns></returns>
        public static Edge[] AccessEdge(Body2 SwBody2)
        {
            object[] edges;
            Edge[] SwEdge = new Edge[0];
            edges = SwBody2.GetEdges();
            int i = 0;
            foreach (Edge TempEdge in edges)
            {
                Array.Resize(ref SwEdge, SwEdge.Length + 1);
                SwEdge[i] = TempEdge;
                i += 1;
            }
            return SwEdge;
        }
        #endregion

        #region Fonctions utilitaires

            #region Cacher les composants
        /// <summary>
        /// Cache un seul Component2
        /// </summary>
        /// <param name="mySwApp"></param>
        /// <param name="SwComponent"></param>
        public static void HideComponentFunction(SldWorks.SldWorks mySwApp, Component2 SwComponent)
        {
            /*INSPIRÉ DE L'AIDE de l'API:
            * Undo Hidden Component and Fire Undo Post-Notify Event Example (C#)*/
            ModelDoc2 swModel = default(ModelDoc2);
            ModelDocExtension swModelDocExt = default(ModelDocExtension);
            bool boolstatus = false;
            swModel = (ModelDoc2)mySwApp.ActiveDoc;
            swModelDocExt = (ModelDocExtension)swModel.Extension;
            //FIN Code Inspiré
            boolstatus = swModelDocExt.SelectByID2(SwComponent.Name2, "COMPONENT", 0, 0, 0, false, 0, null, 0);
            swModel.HideComponent2();
        }

        /// <summary>
        /// Permet de cacher les composants non sélectionnés
        /// Distingue le composant de référence et les 
        /// composants sélectionnés, des non sélectionnés
        /// en faisant un test sur leur ID
        /// </summary>
        /// <param name="mySwApp"></param>
        /// <param name="swComponents"></param>
        /// <param name="swComponentsSelected"></param>
        /// <param name="RefComponent"></param>
        public static void HideComponentsNonSelected(SldWorks.SldWorks mySwApp, Component2[] swComponents, Component2[] swComponentsSelected, Component2 RefComponent)
        {
            int count;
            foreach (Component2 SwComp in swComponents)
            {
                count = 0;
                foreach (Component2 SwCompSelect in swComponentsSelected)
                {
                    if (SwComp.GetID() != SwCompSelect.GetID())
                    {
                        count += 1;
                    }
                }
                if (count == swComponentsSelected.Length)
                {
                    if(SwComp.GetID() != RefComponent.GetID())
                    {
                        HideComponentFunction(mySwApp, SwComp);
                    }
                }
            }
        }
            #endregion

            #region Fonctions de coloriage

        /// <summary>
        /// Fonction permettant de colorier une seule face (Face2)
        /// </summary>
        /// <param name="Face"></param>
        /// <param name="doc"></param>
        /// <param name="couleur1"></param>
        /// <param name="couleur2"></param>
        /// <param name="couleur3"></param>
        public static void ColorFace(Face2 SwFace, ModelDoc2 doc, double couleur1, double couleur2, double couleur3)
        {
            double[] colorInfo = SwFace.MaterialPropertyValues; // On créer une variable qui stockera les inforamtions sur la couleur 

            if (colorInfo == null)
            {
                colorInfo = doc.MaterialPropertyValues;
            }

            colorInfo[0] = couleur1;
            colorInfo[1] = couleur2;
            colorInfo[2] = couleur3;

            SwFace.MaterialPropertyValues = colorInfo; // On affecte la couleur à la face cylindrique
        }

        /// <summary>
        /// Coloriage d'un tableau de Faces (Face2)
        /// </summary>
        /// <param name="Face"></param>
        /// <param name="doc"></param>
        /// <param name="couleur1"></param>
        /// <param name="couleur2"></param>
        /// <param name="couleur3"></param>
        public static void ColorFaceTab(Face2[] Face, ModelDoc2 Swdoc, double couleur1, double couleur2, double couleur3)
        {
            foreach (Face2 SwFace in Face)
            {
                ColorFace(SwFace, Swdoc, couleur1, couleur2, couleur3);
            }
        }

        /// <summary>
        /// Coloriage d'un composant dans un assemblage
        /// Opérationnel si deux pièces sont identiques
        /// et qu'on ne veut colorier qu'une seule
        /// </summary>
        /// <param name="SwComponent"></param>
        /// <param name="doc"></param>
        /// <param name="couleur1"></param>
        /// <param name="couleur2"></param>
        /// <param name="couleur3"></param>
        public static void ColorComponent(Component2 SwComponent, ModelDoc2 doc, double couleur1, double couleur2, double couleur3)
        {
            double[] colorInfo = SwComponent.MaterialPropertyValues; // On créer une variable qui stockera les inforamtions sur la couleur 

            if (colorInfo == null)
            {
                colorInfo = doc.MaterialPropertyValues;
            }

            colorInfo[0] = couleur1;
            colorInfo[1] = couleur2;
            colorInfo[2] = couleur3;

            SwComponent.MaterialPropertyValues = colorInfo;
        }

        /// <summary>
        /// Coloriage multiple de composant d'assemblage
        /// Appel à ColorComponent
        /// </summary>
        /// <param name="SwComponent"></param>
        /// <param name="doc"></param>
        /// <param name="couleur1"></param>
        /// <param name="couleur2"></param>
        /// <param name="couleur3"></param>
        public static void ColorComponents(Component2[] SwComponent, ModelDoc2 doc, double couleur1, double couleur2, double couleur3)
        {
            foreach (Component2 Compo in SwComponent)
            {
                ColorComponent(Compo, doc, couleur1, couleur2, couleur3);
            }
        }

        /// <summary>
        /// Coloriage d'un ModelDoc2 dans un assemblage
        /// Opérationnel si deux pièces sont identiques
        /// et qu'on ne veut colorier qu'une seule
        /// </summary>
        /// <param name="SwComponent"></param>
        /// <param name="doc"></param>
        /// <param name="couleur1"></param>
        /// <param name="couleur2"></param>
        /// <param name="couleur3"></param>
        public static void ColorModelDoc2(Component2 SwComponent, ModelDoc2 doc, double couleur1, double couleur2, double couleur3)
        {
            double[] colorInfo = SwComponent.MaterialPropertyValues; // On créer une variable qui stockera les inforamtions sur la couleur 
            ModelDoc2 SwModelDoc = SwComponent.GetModelDoc2();

            if (colorInfo == null)
            {
                colorInfo = doc.MaterialPropertyValues;
            }

            colorInfo[0] = couleur1;
            colorInfo[1] = couleur2;
            colorInfo[2] = couleur3;

            SwModelDoc.MaterialPropertyValues = colorInfo;
        }

        /// <summary>
        /// Coloriage multiple de ModelDoc2 d'assemblage
        /// Appel à ColorModelDoc2
        /// </summary>
        /// <param name="SwComponent"></param>
        /// <param name="doc"></param>
        /// <param name="couleur1"></param>
        /// <param name="couleur2"></param>
        /// <param name="couleur3"></param>
        public static void ColorModelDoc2s(Component2[] SwComponent, ModelDoc2 doc, double couleur1, double couleur2, double couleur3)
        {
            foreach (Component2 Compo in SwComponent)
            {
                ColorModelDoc2(Compo, doc, couleur1, couleur2, couleur3);
            }
        }

        #endregion

            #region Fonctions pour enlever les couleurs d'un Component2

        /// <summary>
        /// Méthode permettant de retirer la couleur la plus dominante sur une pièce
        /// </summary>
        /// <param name="SwComponent"></param>
        public static void RemoveColor(Component2 SwComponent)
        {
            //Retirer au niveau composant d'assemblage
            bool boolean;
            object vConfig = null;
            boolean = SwComponent.RemoveMaterialProperty2((int)swInConfigurationOpts_e.swThisConfiguration, vConfig);

            //Retirer au niveau ModelDoc2
            
        }

        /// <summary>
        /// Permet de retirer les couleurs d'un tableau de Component2
        /// </summary>
        /// <param name="SwComponents"></param>
        public static void RemoveColorComponents(Component2[] SwComponents)
        {
            foreach(Component2 Compo in SwComponents)
            {
                RemoveColor(Compo);
            }
        }
        #endregion

        #endregion

        #region Méthodes de sélection

        #region Sélection d'un Composant par son ID
        /// <summary>
        /// Méthode de sélection d'un composant par son ID
        /// L'utilosateur doit entrer le nom du composant à extraire en utilisant la forme :
        /// Nom piece-numéro@nomdocument
        /// </summary>
        /// <param name="mySwApp"></param>
        /// <returns></returns>
        public static Component2 ExtractComponentByID(SldWorks.SldWorks mySwApp)
        {
            /*INSPIRÉ DE L'AIDE de l'API:
            * Undo Hidden Component and Fire Undo Post-Notify Event Example (C#)*/
            ModelDoc2 swModel = default(ModelDoc2);
            ModelDocExtension swModelDocExt = default(ModelDocExtension);
            bool boolstatus = false;
            swModel = (ModelDoc2)mySwApp.ActiveDoc;
            swModelDocExt = (ModelDocExtension)swModel.Extension;
            SelectionMgr selectionMgr = (SelectionMgr)swModel.SelectionManager;
            Component2 SwComponent;

            Console.WriteLine("Entrer le nom du composant à extraire en utilisant la forme:");
            Console.WriteLine("Nom piece-numéro@nomdocument");
            Console.WriteLine("Exemple: Piece demo 6<2>  (dans le doc Sous Assemblage 2");
            Console.WriteLine("Ecrire: Piece demo 6-1@Sous assemblage 2");
            string ComponentName = Console.ReadLine();

            boolstatus = swModelDocExt.SelectByID2(ComponentName, "COMPONENT", 0, 0, 0, false, 0, null, 0);
            //FIN Code Inspiré
            SwComponent = (Component2)selectionMgr.GetSelectedObjectsComponent4(1, -1);
            return SwComponent;
        }
            #endregion

            #region Selection Graphique des Components
                
                #region Selection Multiple
        /*CODE EMPUNTÉ ET ADAPTÉ
         * Fonction issue de l'aide l'API
         * Le code est donné dans l'exemple 
         * Move Bodies Example (C#)
         * */
        /// <summary>
        /// Extraction des composants sélectionnés graphiquement ou 
        /// par l'arbre de construction. Si un composant est sélectionné
        /// plusieurs fois (exemple 2 faces sélectionnées), un seul composant
        /// est retourné (comparaison par l'ID).
        /// Si on reselctionne le composant de référence, il garde son statut de
        /// composant de référence, mais n'est pas inclu dans la liste des 
        /// composants sélectionnés
        /// </summary>
        /// <param name="swModel"></param>
        /// <returns></returns>
        public static Component2[] SelectComponents(ModelDoc2 swModel, Component2 RefComponent)
        {
            Console.WriteLine("Sélectionner les composants à étudier en cliquant sur \n une part de l'arbre de construction ou sur face \n maintenir CTRL Gauche pour une sélection multiple");
            Console.WriteLine("Appuyer sur entrée une fois la selection faite");
            Console.ReadLine();
            Component2 componentSelected;
            Component2[] SwComponent = new Component2[0];
            SelectionMgr selectionMgr = (SelectionMgr)swModel.SelectionManager;
            int count = selectionMgr.GetSelectedObjectCount2(-1);
            int j = 0;      //Variable compteur pour le tableau dynamique
            for (int i = 0; i <= count - 1; i++)
            {
                int detect = 0;
                componentSelected = (Component2)selectionMgr.GetSelectedObjectsComponent4(i + 1, -1);
                foreach (Component2 SwComponentTest in SwComponent)
                {
                    if (SwComponentTest.GetID() == componentSelected.GetID() || RefComponent.GetID() == componentSelected.GetID())
                    {
                        detect += 1;
                    }
                }

                if (detect == 0)
                {
                    Array.Resize(ref SwComponent, SwComponent.Length + 1);
                    SwComponent[j] = componentSelected;
                    j += 1;
                }
            }
            return SwComponent;
        }
        /*FIN DU CODE EMPRUNTÉ*/
        #endregion

                #region Selection individuelle
        /*CODE EMPUNTÉ ET ADAPTÉ
         * Fonction issue de l'aide l'API
         * Le code est donné dans l'exemple 
         * Move Bodies Example (C#)
         */
        /// <summary>
        /// Extraction du composant sélectionns graphiquement ou 
        /// par l'arbre de construction. Si un composant est sélectionné
        /// plusieurs fois (exemple 2 faces sélectionnées), un seul composant
        /// est retourné (comparaison par l'ID). 
        /// </summary>
        /// <param name="swModel"></param>
        /// <returns></returns>
        public static Component2 SelectComponent(ModelDoc2 swModel)
        {
            Component2 componentSelected;
            SelectionMgr selectionMgr = (SelectionMgr)swModel.SelectionManager;
            componentSelected = (Component2)selectionMgr.GetSelectedObjectsComponent4(1, -1);
            return componentSelected;
        }
        /*FIN DU CODE EMPRUNTÉ*/
        #endregion

                #region Selection du composant de référence
        /// <summary>
        /// Méthode permettant de gérer la sélection graphique du composant de référence
        /// </summary>
        /// <param name="swModel"></param>
        /// <returns></returns>
        public static Component2 GraphicRefComponentSelection(ModelDoc2 swModel, SldWorks.SldWorks mySwapp)
        {
            Console.WriteLine("Sélectionner la pièce de référence graphiquement dans l’assemblage en cliquant sur \n une part de l'arbre de construction ou sur un object d'une part (arete, face, etc...)");
            Console.WriteLine("Appuyer sur entrée une fois la selection faite");
            Console.ReadLine();
            Component2 SwRefComponentsSelected = (Component2)SelectComponent(swModel);
           // ModelDoc2 MyModelDoc= SwRefComponentsSelected.GetModelDoc2();
            //PartDoc partDoc = ExtractPartDoc(MyModelDoc);
            
            //StandardTexture(partDoc);
            return SwRefComponentsSelected;
        }
        #endregion

        #endregion

        #region Selection Graphique des faces
        /// <summary>
        /// Méthode permettant de gérer la sélection des Faces d'un composants par ID
        /// </summary>
        /// <param name="swModel"></param>
        /// <returns></returns>
        public static int[] GetFace2IDs(Face2[] SwFace)
        {
            int[] SwID = new int[0];
            for (int i = 0; i <= SwFace.Length - 1; i++)
            {
                Array.Resize(ref SwID, SwID.Length + 1);
                SwID[i] = SwFace[i].GetFaceId();
            }
            return SwID;
        }

        /// <summary>
        /// Extraction dans un tableau de toutes les faces (Face2) sélectionnées
        /// </summary>
        /// <param name="swModel"></param>
        /// <returns></returns>
        public static Face2[] SelectFace2(ModelDoc2 swModel)
        {
            Face2 FaceSelected;
            Face2[] SwFace2 = new Face2[0];
            int value = 0;
            SelectionMgr selectionMgr = (SelectionMgr)swModel.SelectionManager;
            int count = selectionMgr.GetSelectedObjectCount2(-1);
            for (int i = 0; i <= count - 1; i++)
            {
                FaceSelected = (Face2)selectionMgr.GetSelectedObject6(i + 1, -1);
                Array.Resize(ref SwFace2, SwFace2.Length + 1);
                FaceSelected.SetFaceId(value += 1);
                value = FaceSelected.GetFaceId();
                SwFace2[i] = FaceSelected;
            }
            return SwFace2;
        }

        /// <summary>
        /// Méthode permettant de gérer la sélection graphique des faces
        /// L'utilisateur doit cliquer sur les faces d'intérêt
        /// </summary>
        /// <param name="swModel"></param>
        /// <returns></returns>
        public static Face2[] GraphicFaceSelection(ModelDoc2 swModel)
        {
            Console.WriteLine("Sélectionner les faces des pièces");
            Console.WriteLine("Appuyer sur entrée une fois la selection faite");
            Console.ReadLine();
            Face2[] SwFaceSelected = (Face2[])SelectFace2(swModel);
            //ColorFaceTab(SwFaceSelected, swModel, 0.5, 0.5, 1);
            return SwFaceSelected;
        }
        #endregion

        #endregion

        #region Méthode de Temporisation
        /// <summary>
        /// Méthode permettant de permet de voir ce que font les méthodes en mode pas à pas, sans utiliser le debugger.
        /// </summary>
        /// <returns></returns>
        public static void RefreshWindow()
        {
            Console.WriteLine("Rafraichir l'affichage en bougeant la vue sur SolidWorks");
            Console.WriteLine("Appuyer sur Entrée une fois l'affichage raffraîchi");
            Console.ReadLine();
        }
        #endregion
    }
}
