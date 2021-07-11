podTemplate( //idleMinutes: 30, 
    yaml: '''
apiVersion: v1
kind: Pod
metadata:
  namespace: jenkins
spec:
  volumes:
    - name: docker-insecure-registries
      configMap:
        name: harbor-allow-insecure-registries
        items:
          - key: daemon.json
            path: daemon.json
    - name: cache
      hostPath:
        path: /tmp
        type: Directory
  serviceAccountName: jenkins-sa
  containers:
  - name: docker
    image: docker:19.03.1-dind
    securityContext:
      privileged: true
    env:
      - name: DOCKER_TLS_CERTDIR
        value: ""
    volumeMounts:
    - name: cache
      mountPath: /var/lib/docker
    - name: docker-insecure-registries
      mountPath: /etc/docker/daemon.json
      subPath: daemon.json
''') {
    node(POD_LABEL) {
        stage("GIT") {
          git credentialsId: 'github-cred', branch: 'main', url: 'https://github.com/alinahid477/vmw-calculator-divisionservice.git'
        }
        
        stage("DOCKER") {
          container('docker') {
            withCredentials([usernamePassword(credentialsId: 'dockerhub-cred', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
              sh """ 
                  df -h
                  df -hi /var/lib/docker
                  docker image ls
                  docker login -u ${USERNAME} -p ${PASSWORD} &&
                  docker build -t harbor-svc.haas-422.pez.vmware.com/anahid/divisionservice:latest .
                  docker logout
              """                
            }
            withCredentials([usernamePassword(credentialsId: 'harbor-cred', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
              sh """
                  docker login -u ${USERNAME} -p ${PASSWORD} harbor-svc.haas-422.pez.vmware.com &&
                  docker push harbor-svc.haas-422.pez.vmware.com/anahid/divisionservice:latest
              """
            }
          }

        }
        
        stage("K8S") {
          withKubeConfig([credentialsId: 'jenkins-robot-token',
                      serverUrl: 'https://192.168.220.7:6443',
                      clusterName: 'calc-k8-cluster',
                      namespace: 'calculator'
                      ]) {
            sh 'curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"'
            sh 'ls -la'
            sh 'chmod 777 ./kubectl'
            sh './kubectl apply -f kubernetes/deployment.yaml'
            //sh './kubectl patch deployment divisionservice-deploy -p \"{\\"spec\\": {\\"template\\": {\\"metadata\\": { \\"labels\\": {  \\"redeploy\\": \\"$(date +%s)\\"}}}}}\" -n calculator'
          }
        }
          
        
    }
}