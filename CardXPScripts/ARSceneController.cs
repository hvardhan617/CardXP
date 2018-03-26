using ChartAndGraph;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TransitionalObjects;
using UnityEngine;
using UnityEngine.UI;

//Handle the sequence of Animations using Couroutines,only without putting any delay on Transitional Objects Setup of GameObjects.
//setActive(true) associated to any gameobject automatically invokes the animation(initialise()) of transitional Object,
//hence dont call it explicitly unless any change caught by Firebase Listeners in Backend.
public class ARSceneController : MonoBehaviour
{
    public RemoteConfigManager remoteConfigManager;
    private RemoteConfigParameters remoteParameters;

    public GameObject week1;
    public GameObject week2;
    public GameObject week3;
    public GameObject week4;

    public GameObject leftPlane;
    public GameObject rightPlane;
    public GameObject lowerPlane;
    public GameObject upperPlane;

    public GameObject leftMenu;
    public GameObject rightMenu;
    public GameObject lowerMenu;

    public GameObject sideMenu;


    //Buttons within Individual menus must be created and instantiated at runtime dynamically,
    //Hence we use already configured button prefabs and instantiate when necessary.
    //under lower menu
    public Button b1;
    public Button b2;
    public Button b3;
    public Button b4;
    // public Button b5;

    //public GameObject balanceButtonRowPrefab;
    //public GameObject scrollContainer;

    //under right menu
    public Button b6;
    public Button b7;
    public Button b8;

    // public GameObject duesButtonRowPrefab;

    //under left menu
    public Button b9;
    public Button b10;
    public Button b11;

    public Text rewardPointsText;
    public Text currentBalanceValue;


    // public GameObject pastTransPieChart;
    public GameObject billsPieChart;


    //public Text week1text;
    //public Text week2text;
    //public Text week3text;
    //public Text week4text;

    //public GameObject accountTypes_pieChart;
    public Material elecMat;
    public Material ccMat;
    public Material teleMat;

    public GameObject weeklyExpensesCanvas;
    public GameObject videoPlayerCanvas;
    public GameObject offersDisplayCanvas;


    //public GameObject rewardPoints;

    // public string loginId;
    //Firebase.Auth.FirebaseUser currentUser;
    //string loggedInUser = null;

    private void Awake()
    {
        // loggedInUser = currentUser.UserId;

        //CustomerInfo customerInfo = new CustomerInfo();
        //string key = Router_Manager.baseRef.Push().Key;
        //CreateNewCustomerNodes(customerInfo, key);
        Debug.Log("Awake of ARScene Controller");


        remoteParameters = new RemoteConfigParameters();
        //accountTypes_pieChart.SetActive(false);
        //cube.SetActive(false);

        RootObject rootObject = new RootObject();

        //DatabaseManager.sharedInstance.FetchCustomerInfo(result =>
        //{
        //    rootObject = result;
        //    Debug.Log(rootObject.customer.email);
        //}, currentUser.UserId);


        //remoteConfigManager.FetchData();
        //remoteParameters=remoteConfigManager.DisplayData(remoteParameters);
        //b1.GetComponent<Image>().color = new Color(remoteParameters.lowerMenuB1_R, remoteParameters.lowerMenuB1_G, remoteParameters.lowerMenuB1_B,255);
        //b2.GetComponent<Image>().color = new Color(remoteParameters.lowerMenuB2_R, remoteParameters.lowerMenuB2_G, remoteParameters.lowerMenuB2_B, 255);
        //b3.GetComponent<Image>().color = new Color(remoteParameters.lowerMenuB3_R, remoteParameters.lowerMenuB3_G, remoteParameters.lowerMenuB3_B, 255);
        //b4.GetComponent<Image>().color = new Color(remoteParameters.lowerMenuB4_R, remoteParameters.lowerMenuB4_G, remoteParameters.lowerMenuB4_B, 255);


        upperPlane.SetActive(false);
        lowerPlane.SetActive(false);
        leftPlane.SetActive(false);
        rightPlane.SetActive(false);
        week1.SetActive(false);
        week2.SetActive(false);
        week3.SetActive(false);
        week4.SetActive(false);
        leftMenu.SetActive(false);
        rightMenu.SetActive(false);
        lowerMenu.SetActive(false);
        sideMenu.SetActive(false);

        b1.gameObject.SetActive(false);
        b2.gameObject.SetActive(false);
        b3.gameObject.SetActive(false);
        b4.gameObject.SetActive(false);
        //b5.gameObject.SetActive(false);
        b6.gameObject.SetActive(false);
        b7.gameObject.SetActive(false);
        b8.gameObject.SetActive(false);
        b9.gameObject.SetActive(false);
        b10.gameObject.SetActive(false);
        b11.gameObject.SetActive(false);
        // rewardPointsText.gameObject.SetActive(false);


        // pastTransPieChart.SetActive(false);
        billsPieChart.SetActive(false);
        weeklyExpensesCanvas.SetActive(false);
        videoPlayerCanvas.SetActive(false);
        offersDisplayCanvas.SetActive(false);







    }
    // Use this for initialization
    void Start()
    {

        Debug.Log("Start of ARScene Controller");
        //Router_Manager.baseRef.Child("balanceInfo").Child("/7MMLJ9DHQq").ChildAdded += NewChildAdded_BalanceInfo;
        //Router_Manager.baseRef.Child("balanceInfo").Child("/7MMLJ9DHQq/").Child("/b1").ValueChanged += ValueChanged_BalanceInfo;

        //Debug.Log("Activating GameObjects");
        //StartCoroutine(ActivatePanels());

    }

    public void InitializeUI()
    {
        Debug.Log("Initialized of ARScene Controller");
        // RootObject rootObject = new RootObject();
        DatabaseManager.sharedInstance.FetchCustomerInfo(result =>
        {
            RootObject rootObject = new RootObject();

            rootObject = result;
            Debug.Log("Inside AR scene before PIE chart" + rootObject.customer.email);
            //setupPieChart(rootObject);
            //    setupPieChart(rootObject);
        }, "7MMLJ9DHQq");

        //RouterManager.baseRef.Child("balanceInfo").Child("/7MMLJ9DHQq").ChildAdded += NewChildAdded_BalanceInfo;
        RouterManager.baseRef.Child("balanceInfo/").Child("7MMLJ9DHQq").Child("/b1").ValueChanged += OnValueChanged_BalanceInfo_b1;
        RouterManager.baseRef.Child("balanceInfo/").Child("7MMLJ9DHQq").Child("/b2").ValueChanged += OnValueChanged_BalanceInfo_b2;
        RouterManager.baseRef.Child("balanceInfo/").Child("7MMLJ9DHQq").Child("/b3").ValueChanged += OnValueChanged_BalanceInfo_b3;
        RouterManager.baseRef.Child("balanceInfo/").Child("7MMLJ9DHQq").Child("/b4").ValueChanged += OnValueChanged_BalanceInfo_b4;

        RouterManager.baseRef.Child("dues/").Child("7MMLJ9DHQq").Child("/dues_Electricity").ValueChanged += OnValueChanged_dues_Electricity;
        RouterManager.baseRef.Child("dues/").Child("7MMLJ9DHQq").Child("/dues_CreditCard").ValueChanged += OnValueChanged_dues_CreditCard;
        RouterManager.baseRef.Child("dues/").Child("7MMLJ9DHQq").Child("/dues_Telephone").ValueChanged += OnValueChanged_dues_Telephone;

        RouterManager.baseRef.Child("rewardPoints/").Child("7MMLJ9DHQq").ChildChanged += OnChildChanged_rewardPoints;

        // RouterManager.baseRef.Child("dues/").Child("7MMLJ9DHQq").ChildChanged += OnChildChanged_dues;
        // RouterManager.baseRef.Child("balanceInfo/").Child("7MMLJ9DHQq").ChildChanged += OnChildChanged_PastTransactions;

        RouterManager.baseRef.Child("balanceInfo/").Child("7MMLJ9DHQq").Child("/currentBalance").ValueChanged += OnValueChanged_CurrentBalance;


        //Debug.Log("Inside AR scene before PIE chart" + rootObject.customer.email);
        //CreateNewRecentTransactionsButton();
        StartCoroutine(SetupUI());
        //return rootObject;

    }

    private void OnValueChanged_dues_Telephone(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            Debug.Log("Value changed has been detected at the Dues Node!" + args.Snapshot.Value);
            PieChart pie = billsPieChart.GetComponent<PieChart>();
            double newPhoneBill = Convert.ToDouble(args.Snapshot.Value);
            // pie.DataSource.AddCategory("Deposit", mat);
            // pie.DataSource.SlideValue("Deposit", 70, 10f);
            // pie.DataSource.SetValue("Deposit", newlyAddedValue);
            pie.DataSource.SetValue("Telephone", newPhoneBill);
            PieAnimation pieAnimation = billsPieChart.GetComponent<PieAnimation>();
            pieAnimation.Animate();
        }
    }

    private void OnValueChanged_dues_CreditCard(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            Debug.Log("Value changed has been detected at the Dues Node!" + args.Snapshot.Value);
            PieChart pie = billsPieChart.GetComponent<PieChart>();
            double newCCBill = Convert.ToDouble(args.Snapshot.Value);
            // pie.DataSource.AddCategory("Deposit", mat);
            // pie.DataSource.SlideValue("Deposit", 70, 10f);
            // pie.DataSource.SetValue("Deposit", newlyAddedValue);
            pie.DataSource.SetValue("CreditCard", newCCBill);
            PieAnimation pieAnimation = billsPieChart.GetComponent<PieAnimation>();
            pieAnimation.Animate();
        }
    }

    private void OnValueChanged_dues_Electricity(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            Debug.Log("Value changed has been detected at the Dues Node!" + args.Snapshot.Value);
            PieChart pie = billsPieChart.GetComponent<PieChart>();
            double newElecBill = Convert.ToDouble(args.Snapshot.Value);
            // pie.DataSource.AddCategory("Deposit", mat);
            // pie.DataSource.SlideValue("Deposit", 70, 10f);
            // pie.DataSource.SetValue("Deposit", newlyAddedValue);
            pie.DataSource.SetValue("Electricity", newElecBill);
            PieAnimation pieAnimation = billsPieChart.GetComponent<PieAnimation>();
            pieAnimation.Animate();
        }
    }

    private void OnValueChanged_BalanceInfo_b4(object sender, ValueChangedEventArgs e)
    {

        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            Text b4changed = b4.GetComponent<Text>();
            DataSnapshot data = e.Snapshot;
            b4changed.text = Convert.ToString(e.Snapshot.Value);
        }
    }

    private void OnValueChanged_BalanceInfo_b3(object sender, ValueChangedEventArgs e)
    {

        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            Text b3changed = b3.GetComponent<Text>();
            DataSnapshot data = e.Snapshot;
            b3changed.text = Convert.ToString(e.Snapshot.Value);
        }
    }

    private void OnValueChanged_BalanceInfo_b2(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            Text b2changed = b2.GetComponent<Text>();
            DataSnapshot data = e.Snapshot;
            b2changed.text = Convert.ToString(e.Snapshot.Value);
        }
    }

    private void OnValueChanged_CurrentBalance(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = e.Snapshot;
            currentBalanceValue.text = Convert.ToString(e.Snapshot.Value);
        }
    }

    void OnChildChanged_rewardPoints(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        DataSnapshot data = args.Snapshot;

        rewardPointsText.text = Convert.ToString(args.Snapshot.Value);

        // Do something with the data in args.Snapshot
    }



    //    void OnChildChanged_PastTransactions(object sender, ChildChangedEventArgs args)
    //{
    //    Text b1Text = b1.GetComponent<Text>();
    //    Text b2Text = b2.GetComponent<Text>();
    //    Text b3Text = b3.GetComponent<Text>();
    //    if (args.DatabaseError != null)
    //    {
    //        Debug.LogError(args.DatabaseError.Message);
    //        return;
    //    }
    //    // Do something with the data in args.Snapshot
    //    b1Text.text = Convert.ToString(args.Snapshot.Value);


    //}

    //private void OnChildChanged_PastTransactions(object sender, ChildChangedEventArgs e)
    //{
    //    throw new NotImplementedException();
    //}

    //private void OnChildChanged_dues(object sender, ChildChangedEventArgs e)
    //{
    //    throw new NotImplementedException();
    //}



    //void NewChildAdded_BalanceInfo(object sender, ChildChangedEventArgs args)
    //{
    //    if (args.Snapshot.Value == null)
    //    {
    //        Debug.Log("Sorry, there was no data at that node.");
    //    }
    //    else
    //    {
    //        Debug.Log("New child has been added at the balanceInfo Node!" + args.Snapshot.Value);
    //        PieChart pie = accountTypes_pieChart.GetComponent<PieChart>();
    //        double newlyAddedValue = Convert.ToDouble(args.Snapshot.Value);
    //        pie.DataSource.AddCategory("Deposit", mat);
    //        pie.DataSource.SlideValue("Deposit", 70, 10f);
    //        pie.DataSource.SetValue("Deposit", newlyAddedValue);
    //        PieAnimation pieAnimation = accountTypes_pieChart.GetComponent<PieAnimation>();
    //        pieAnimation.Animate();
    //    }
    //}

    //void ValueChanged_BalanceInfo(object sender, ValueChangedEventArgs args)
    //{
    //    if (args.Snapshot.Value == null)
    //    {
    //        Debug.Log("Sorry, there was no data at that node.");
    //    }
    //    else
    //    {
    //        Debug.Log("Value has been changed at the balanceInfo Node!" + args.Snapshot.Value);
    //        PieChart pie = accountTypes_pieChart.GetComponent<PieChart>();
    //        double changedValue = Convert.ToDouble(args.Snapshot.Value);
    //        pie.DataSource.SetValue("Savings", changedValue);
    //        PieAnimation pieAnimation = accountTypes_pieChart.GetComponent<PieAnimation>();
    //        pieAnimation.Animate();


    //        Vector3 end = new Vector3(1, 7, 1);
    //        cube.GetComponent<MovingTransition>().endPoint = end;
    //        cube.GetComponent<MovingTransition>().triggerInstantly = true;
    //    }
    //}

    void setupPieChart(/*RootObject rootObject*/)
    {
        //billsPieChart.SetActive(true);
        //  Debug.Log("TESTING:::::"+ rootObject.dues.dues_Electricity.ToString());
        CustomerInfo cust = new CustomerInfo();

        //double elecBillAmt = Convert.ToDouble(rootObject.dues.dues_Electricity.ToString());
        //double cardBillAmt = Convert.ToDouble(rootObject.dues.dues_CreditCard.ToString());
        //double phoneBillAmt = Convert.ToDouble(rootObject.dues.dues_Telephone.ToString());

        double elecBillAmt = Convert.ToDouble(cust.dues_Electricity.ToString());
        double cardBillAmt = Convert.ToDouble(cust.dues_CreditCard.ToString());
        double phoneBillAmt = Convert.ToDouble(cust.dues_Telephone.ToString());

        //  double currentBal = Convert.ToDouble(rootObject.balance.b2);

        PieChart pie = billsPieChart.GetComponent<PieChart>();
        if (pie != null)
        {

            pie.DataSource.AddCategory("Electricity", elecMat);
            pie.DataSource.AddCategory("CreditCard", ccMat);
            pie.DataSource.AddCategory("Telephone", teleMat);

            // pie.DataSource.SlideValue()
            pie.DataSource.SlideValue("Electricity", 50, 15f);
            pie.DataSource.SetValue("Electricity", elecBillAmt);

            pie.DataSource.SlideValue("CreditCard", 50, 10f);
            pie.DataSource.SetValue("CreditCard", cardBillAmt);

            pie.DataSource.SlideValue("Telephone", 50, 10f);
            pie.DataSource.SetValue("Telephone", phoneBillAmt);

            int categories = pie.DataSource.TotalCategories;

            Debug.Log("Pie Chart Categories:::" + categories);

        }
    }

    //private void setupBarGraph(RootObject rootObject)
    //{
    //    cube.SetActive(true);

    //    //  BaseTransition[] transitions1 = new BaseTransition[5];
    //    // ((MovingTransition)transitions[0]).startPoint
    //    // transitions1 = cube.GetComponent<TransitionalObject>().transitions;
    //    Vector3 start = new Vector3(1, 1, 1);
    //    start = cube.GetComponent<MovingTransition>().startPoint;
    //    Vector3 ender = new Vector3(1, 3, 1);
    //    ender = cube.GetComponent<MovingTransition>().endPoint;
    //}
    IEnumerator SetupUI()
    {
        Debug.Log("Activating UI GameObjects On AR Scene Intialization");
        upperPlane.SetActive(true);
        leftPlane.SetActive(true);
        rightPlane.SetActive(true);
        lowerPlane.SetActive(true);

        yield return new WaitForSeconds(2);
        lowerMenu.SetActive(true);
        rightMenu.SetActive(true);
        leftMenu.SetActive(true);

        sideMenu.SetActive(true);
        yield return new WaitForSeconds(1);

        b1.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        b2.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        b3.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        b4.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //b5.gameObject.SetActive(true);
        //yield return new WaitForSeconds(1);

        setupPieChart();
        billsPieChart.SetActive(true);
        yield return new WaitForSeconds(1);

        b6.gameObject.SetActive(true);
        b9.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        b7.gameObject.SetActive(true);
        b10.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        b8.gameObject.SetActive(true);
        b11.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        rewardPointsText.gameObject.SetActive(true);


        //week1text.gameObject.SetActive(true);
        weeklyExpensesCanvas.SetActive(true);

        week1.SetActive(true);
        week2.SetActive(true);
        week3.SetActive(true);
        week4.SetActive(true);
        yield return new WaitForSeconds(1);
        //week2text.gameObject.SetActive(true);
        //week2.SetActive(true);
        //yield return new WaitForSeconds(1);
        ////week3text.gameObject.SetActive(true);
        //week3.SetActive(true);
        //yield return new WaitForSeconds(1);
        ////week4text.gameObject.SetActive(true);
        //week4.SetActive(true);
        //yield return new WaitForSeconds(1);






        //    upperPanel.SetActive(true);
        //    //pieChart.SetActive(true);
        //    //yield return new WaitForSeconds(4);
    }

    void CreateNewRecentTransactionsButton()
    {
        for (int i = 1; i <= 5; i++)
        {
            //GameObject newRow = Instantiate(balanceButtonRowPrefab) as GameObject;
            //newRow.transform.SetParent(scrollContainer.transform, false);
            //newRow.GetComponent<MovingTransition>().Initialise();
        }
    }

    void OnValueChanged_BalanceInfo_b1(object sender, ValueChangedEventArgs args)
    {
        b1.gameObject.SetActive(false);
        if (args.Snapshot.Value == null)
        {
            Debug.Log("Sorry, there was no data at that node.");
        }
        else
        {
            Debug.Log("Value has been changed at the balanceInfo Node!" + args.Snapshot.Value);

            string value = args.Snapshot.Value.ToString();
            b1.GetComponent<Text>().text = value;
            b1.gameObject.SetActive(true);
            b1.GetComponent<MovingTransition>().Initialise();
        }
    }

    public VideoFromURL videoFromURL;
    public void onVideoPlayerClick()
    {
        videoPlayerCanvas.SetActive(true);
        videoFromURL.loadVideo();

        StartCoroutine(disableVideo());

    }

    public CloudStorage cloudStorage;
    public void onOffersButtonClick()
    {
        offersDisplayCanvas.SetActive(true);
        cloudStorage.fetchImage();

        StartCoroutine(disableImage());
    }

    private IEnumerator disableImage()
    {
        yield return new WaitForSeconds(10);
        offersDisplayCanvas.SetActive(false);
    }

    private IEnumerator disableVideo()
    {
        yield return new WaitForSeconds(20);
        videoPlayerCanvas.SetActive(false);
    }
}

