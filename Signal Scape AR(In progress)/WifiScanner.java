package com.example.unityjavahelper;

import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.util.Log;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
public class WifiScanner
{

    private IScanResult iScan;
    private WifiManager wifiManager;

    public WifiScanner (Context context, IScanResult iScan)
    {
        this.iScan = iScan;
        wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);

        IntentFilter intentFilter = new IntentFilter();
        intentFilter.addAction(WifiManager.SCAN_RESULTS_AVAILABLE_ACTION);
        context.registerReceiver(receiver, intentFilter);
    }

    public boolean startScanning ()
    {
        boolean success = wifiManager.startScan();
        Log.d("UnityHelper", "Scanning started with result: " + success);
        return success;

    }

    public void startGetRssi (String address)
    {
        WifiInfo wifiInfo = wifiManager.getConnectionInfo();
        if (wifiInfo.getBSSID() != null && wifiInfo.getBSSID().equals(address)) {
            UnityDevice device = new UnityDevice(address, wifiInfo.getSSID(), wifiInfo.getRssi());
            iScan.onScanResultGet(Collections.singletonList(device));
            Log.d("UnityHelper", "Device already connected, no need to call startScanning ");
        } else {
            startScanning();
            Log.d("UnityHelper", "Device is not connected, have toscan for all devices ");
        }
    }


    private BroadcastReceiver receiver = new BroadcastReceiver()
    {
        @Override
        public void onReceive (Context context, Intent intent)
        {
            Log.d("UnityHelper", "onReceive");

            try
            {
                if (intent.getBooleanExtra(WifiManager.EXTRA_RESULTS_UPDATED, false))
                {
                    @SuppressLint("MissingPermission") List<ScanResult> scanResults = wifiManager.getScanResults();
                    List<UnityDevice> devices = new ArrayList<>();

                    for (ScanResult scanResult : scanResults)
                    {
                        UnityDevice device = new UnityDevice(scanResult.BSSID, scanResult.SSID, scanResult.level);
                        devices.add(device);
                    }

                    iScan.onScanResultGet(devices);
                }
                else
                {
                    Log.d("UnityHelper", "Scan results not updated");
                }
            } catch (Exception e)
            {
                Log.w("UnityHelper", e.getMessage());
            }
        }
    };
}
